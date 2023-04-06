using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBuffBar : MonoBehaviour
{
    public Transform Belt;
    public Player Player;
    private void Update()
    {
        //버프 리스트를 받아와서
        //실제로 보이게 함.
        //근데 매 프레임 Instantiate를 하는 건 낭비가 심하니까
        //최초 생성은 하되 삭제는 하지 말고 돌려쓰자.
    }


}
