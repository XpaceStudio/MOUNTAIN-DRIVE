using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purchansebutton : MonoBehaviour
{
   public enum Purchasetype { removerads,coin1000,coin2500,coin6000,stone25,stone75,stone200};
    public Purchasetype purchasetype;

    public void clickPurchase()
    {
        switch (purchasetype)
        {
            case Purchasetype.removerads:
                IAPManager.instance.Buyremove_Ads();
               break;
            case Purchasetype.coin1000:
                IAPManager.instance.coin_1000();
                break;
            case Purchasetype.coin2500:
                IAPManager.instance.coin_2500();
                break;
            case Purchasetype.coin6000:
                IAPManager.instance.coin_6000();
                break;
            case Purchasetype.stone200:
                IAPManager.instance.stone_200();
                break;
            case Purchasetype.stone25:
                IAPManager.instance.stone_25();
                break;
            case Purchasetype.stone75:
                IAPManager.instance.stone_75();
                break;
        }
    }

}
