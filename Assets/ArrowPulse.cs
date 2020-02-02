using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowPulse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var seq = DOTween.Sequence();
        seq.Append(transform.DOPunchScale(Vector3.one * 0.1f, 0.2f));
        seq.SetDelay(1).SetLoops(-1);
    }

}
