using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DeskScript : MonoBehaviour
{
    Vector2 normalPos;
    Vector3 normalDeskPos;
    Vector2 articlePos = new Vector3 (-269, 0);
    Vector3 articleDeskPos = new Vector3(10, -78, 0);

    RectTransform rt;
    private Boolean atArticle;
    public GameObject desk;
    public GameObject seconddesk;
    public GameObject pen;
    public GameObject article;
    public TextMeshProUGUI articleText;

    public GameObject report;
    public GameObject submit;
    // Start is called before the first frame update
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
       // seconddesk = gameObject.transform.Find("SecondDesk").gameObject;
        desk = gameObject.transform.Find("Desk").gameObject;
        pen = gameObject.transform.Find("quill pen").gameObject;
        article = gameObject.transform.Find("Article").gameObject;
        report = gameObject.transform.Find("ScoreReport").gameObject;
        normalPos = rt.anchoredPosition;
        normalDeskPos = seconddesk.GetComponent<RectTransform>().anchoredPosition;
    }

    public void ToArticle(){
        //seconddesk.SetActive(true);
        atArticle = true;
        StopAllCoroutines();
        StartCoroutine(MoveDesk(normalPos, articlePos));
        report.GetComponent<RectTransform>().anchoredPosition = new Vector2(296f, -3.6f);
        submit.GetComponent<RectTransform>().anchoredPosition = new Vector2(296f, -3.6f);
    }
    public void ToRegularDesk(){
         Debug.Log("starterrp");
        pen.SetActive(true);
       // desk.SetActive(true);
        atArticle = false;
        StopAllCoroutines();
        StartCoroutine(MoveDesk(articlePos, normalPos));
        report.GetComponent<RectTransform>().anchoredPosition = new Vector2(296f, -3.6f);
        submit.GetComponent<RectTransform>().anchoredPosition = new Vector2(296f, -3.6f);
    }

    private IEnumerator MoveDesk(Vector2 startPos, Vector2 endPos){
        Debug.Log("startinglerp");
        float moveSpeed = 300f;
        float journeyLength = Vector2.Distance(startPos, endPos);
        Debug.Log(journeyLength);
        float startTime = Time.time;
        
        
        while(rt.anchoredPosition != endPos){
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractJourney = distCovered / journeyLength;
            rt.anchoredPosition = Vector2.Lerp(startPos, endPos, fractJourney);
            yield return null;
        }
        if(atArticle){
            Debug.Log("at");
           pen.SetActive(false);
           article.SetActive(true);

        }
        else{
            seconddesk.SetActive(false);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        articleText.text = ArticleManager.getArticle();
    }
}
