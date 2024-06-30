using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Event]
public class UpdateBagEvent : AEvent<UpdateBag>
{
    protected override void Run(UpdateBag a)
    {
        Start().Forget();
    }


    async UniTask Start()
    {
        GameObject[] BagHolls = GameObject.FindGameObjectsWithTag("BagHoll");
        if (BagHolls.Length > 0)
        {
            foreach (BagItem bagItem in Bag.Instance.bagItems)
            {
                int sortID = bagItem.sortID;
                Transform bagHollTransform = BagHolls[sortID].transform;


                for (int i = 0; i < bagHollTransform.childCount; i++)
                {
                    Transform trans = bagHollTransform.GetChild(i);
                    GameObject.Destroy(trans.gameObject);
                }

                GameObject item = ObjectCreater.CreateEffect("Objects/item", Vector3.zero, 1, BagHolls[sortID].transform);
                Image image = item.GetComponent<Image>();
                string spritePath;
                ItemMatch.list.TryGetValue(bagItem.name, out spritePath);
                Sprite sp = Resources.Load<Sprite>(spritePath);
                image.sprite = sp;
                image.color = Color.white;
                TextMeshProUGUI text = image.GetComponentInChildren<TextMeshProUGUI>();
                text.text = bagItem.num.ToString();
            }
        }
    }
}
