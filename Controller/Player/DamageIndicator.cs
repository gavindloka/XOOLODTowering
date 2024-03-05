using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DamageIndicator : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float LifeTime = 0.6f;
    public float MinDist = 2f;
    public float MaxDist = 3f;

    private Vector3 initPos;
    private Vector3 targetPos;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(2*transform.position - Camera.main.transform.position);
        float direction = Random.rotation.eulerAngles.z;
        initPos = transform.position;
        float dist = Random.Range(MinDist, MaxDist);
        targetPos = initPos + (Quaternion.Euler(0, 0, direction) * new Vector3(dist, dist, 0f));
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float fraction = LifeTime / 2f;
        if (timer > LifeTime) Destroy(gameObject);
        else if (timer > fraction) text.color = Color.Lerp(text.color, Color.clear,(timer-fraction)/(LifeTime - fraction));


        transform.position = Vector3.Lerp(initPos,targetPos,Mathf.Sin(timer/LifeTime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / LifeTime));
    }
    public void SetDamageText(int damage)
    {
        text.text = damage.ToString();
    }
}
