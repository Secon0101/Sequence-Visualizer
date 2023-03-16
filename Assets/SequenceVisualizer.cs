using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SequenceVisualizer : MonoBehaviour
{
    [SerializeField]
    private float delay = 0.1f;
    
    [Space]
    [SerializeField]
    private Transform sequence;
    [SerializeField]
    private Transform element;
    [SerializeField]
    private Text numberText;
    [SerializeField]
    private Slider numberSlider;
    [SerializeField]
    private Text buttonText;
    [SerializeField]
    private Text xText;
    [SerializeField]
    private Text yText;
    
    private int number;
    private bool isRunning;
    
    
    private void Awake()
    {
        OnNumberChanged(numberSlider.value);
    }
    
    /// <summary> 점화식 </summary>
    private static float RecurrenceRelation(int n) 
    {
        // (4^(n+1) - 3^n) / (4^n - 3^n)
        // return (Mathf.Pow(4f, n+1) - Mathf.Pow(3f, n)) / (Mathf.Pow(4f, n) - Mathf.Pow(3f, n));
        // 5(-9/10)^n
        return 5f * Mathf.Pow(-9f / 10f, n);
    }
    
    public void StartSequence() // 시작 버튼 이벤트 
    {
        if (isRunning)
        {
            StopSequence();
            return;
        }
        
        // 초기화
        while (sequence.childCount > 0)
        {
            DestroyImmediate(sequence.GetChild(0).gameObject);
        }
        
        isRunning = true;
        buttonText.text = "중지";
        numberSlider.interactable = false;
        StartCoroutine(SequenceRoutine());
    }
    
    private IEnumerator SequenceRoutine()
    {
        for (var n = 1; n <= number && isRunning; n++)
        {
            var an = RecurrenceRelation(n);
            if (float.IsInfinity(an))
            {
                Debug.Log("Overflow");
                break;
            }
            var pos = new Vector3(n, an);
            transform.position = pos;
            Instantiate(element, pos, Quaternion.identity, sequence);
            xText.text = $"n = {n}";
            yText.text = $"{an:0.0000}";
            
            yield return new WaitForSeconds(delay);
        }
        
        StopSequence();
    }
    
    private void StopSequence()
    {
        isRunning = false;
        buttonText.text = "생성";
        numberSlider.interactable = true;
    }
    
    public void OnDelayChanged(float value)
    {
        delay = value;
    }
    
    public void OnNumberChanged(float value)
    {
        number = (int)value;
        numberText.text = $"n -> {value}";
    }
}
