using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitBagHoll : MonoSingleton<InitBagHoll>
{
    public GameObject bagItem;
    private RectTransform rectTransform;
    public float bagWidth = 1000;
    public float bagHeight = 600;
    private float padding = 30;
    public int bagNum = 10;
    public float splitW = 15;
    public float splitH = 15;

    public GameObject[] bagHolls;
    private Image img;

    public bool isInit;

    private void Awake()
    {
        Bag.Instance.LoadFromFile();
        img = gameObject.GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = bagItem.GetComponent<RectTransform>();
        initBag();
        isInit = true;
    }

    public void initBag()
    {
        float leftBagW = bagWidth - padding * 2 - rectTransform.rect.width;
        float leftBagH = bagHeight - padding * 2 - rectTransform.rect.height;
        float bagItemW = rectTransform.rect.width;
        float bagItemH = rectTransform.rect.height;
        float startX = rectTransform.anchoredPosition3D.x;
        float startY = rectTransform.anchoredPosition3D.y;

        bagHolls = new GameObject[bagNum];
        for (int i = 0; i < bagNum; i++)
        {
            GameObject cloneItem = newItem();
            bagSortID sortID = cloneItem.GetComponent<bagSortID>();
            sortID.sortID = i + 1;
            leftBagW -= bagItemW;
            if (leftBagW > bagItemW + splitH)
            {
                startX += bagItemW + splitW;
            }
            else
            {
                startX = rectTransform.anchoredPosition3D.x;
                startY -= bagItemH + splitH;
                leftBagH -= bagItemH;
                leftBagW = bagWidth - padding * 2 - rectTransform.rect.width;
            }
            RectTransform clonedImageRectTransform = cloneItem.GetComponent<RectTransform>();
            clonedImageRectTransform.anchoredPosition = new Vector3(startX, startY, 0);
            showObjectImage(cloneItem);
            bagHolls[i] = cloneItem;
        }
        showObjectImage(bagItem);
        img.color = Color.white;
        gameObject.SetActive(false);
    }

    private GameObject newItem()
    {
        GameObject clonedItem = Instantiate(bagItem, bagItem.transform.parent);
        return clonedItem;
    }

    private void showObjectImage(GameObject gObj)
    {
        Image imgC = gObj.GetComponent<Image>();
        imgC.color = Color.white;
    }

}
