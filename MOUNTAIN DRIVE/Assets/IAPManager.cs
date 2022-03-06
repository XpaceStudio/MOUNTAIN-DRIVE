using System;
using UnityEngine;
using UnityEngine.Purchasing;


public class IAPManager : MonoBehaviour, IStoreListener
{
    public static IAPManager instance;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    //Step 1 create your products
    private string removeAds = "remove_Ads";
    private string stone25 = "stone_25";
    private string stone75 = "stone_75";
    private string stone200 = "stone_200";
    private string coin1000 = "coin1000";
    private string coin2500 = "coin2500";
    private string coin6000 = "coin6000";



    //************************** Adjust these methods **************************************
    public void InitializePurchasing()
    {
        if (IsInitialized()) { return; }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Step 2 choose if your product is a consumable or non consumable
        builder.AddProduct(removeAds, ProductType.NonConsumable);
        builder.AddProduct(stone25, ProductType.Consumable);
        builder.AddProduct(stone75, ProductType.Consumable);
        builder.AddProduct(stone200, ProductType.Consumable);
        builder.AddProduct(coin1000, ProductType.Consumable);
        builder.AddProduct(coin2500, ProductType.Consumable);
        builder.AddProduct(coin6000, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    //Step 3 Create methods

    public void Buyremove_Ads()
    {
        BuyProductID(removeAds);
    }

    public void stone_25()
    {
        BuyProductID(stone25);
    }
    public void stone_75()
    {
        BuyProductID(stone75);
    }
    public void stone_200()
    {
        BuyProductID(stone200);
    }
    public void coin_2500()
    {
        BuyProductID(coin2500);
    }
    public void coin_1000()
    {
        BuyProductID(coin1000);
    }
    public void coin_6000()
    {
        BuyProductID(coin6000);
    }




    //Step 4 modify purchasing
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, removeAds, StringComparison.Ordinal))
        {
            Debug.Log("remove ads succfully");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, stone25, StringComparison.Ordinal))
        {
            Debug.Log("stone25 succfully");
            int n = PlayerPrefs.GetInt("stone", 0);
            n += 25;
            PlayerPrefs.SetInt("stone", n);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, stone75, StringComparison.Ordinal))
        {
            Debug.Log("stone75 succfully");
            int n = PlayerPrefs.GetInt("stone", 0);
            n += 75;
            PlayerPrefs.SetInt("stone", n);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, stone200, StringComparison.Ordinal))
        {
            Debug.Log("stone200 succfully");
            int n = PlayerPrefs.GetInt("stone", 0);
            n += 200;
            PlayerPrefs.SetInt("stone", n);
        }
        else if(String.Equals(args.purchasedProduct.definition.id, coin1000, StringComparison.Ordinal))
        {
            Debug.Log("coin1000 succfully");
            int n = PlayerPrefs.GetInt("gold", 0);
            n += 1000;
            PlayerPrefs.SetInt("gold", n);
        }
        else if(String.Equals(args.purchasedProduct.definition.id, coin2500, StringComparison.Ordinal))
        {
            Debug.Log("coin2500 added");
            int n = PlayerPrefs.GetInt("gold", 0);
            n += 2500;
            PlayerPrefs.SetInt("gold", n);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, coin6000, StringComparison.Ordinal))
        {
            Debug.Log("coin6000 added");
            int n = PlayerPrefs.GetInt("gold", 0);
            n += 6000;
            PlayerPrefs.SetInt("gold", n);
        }
        else
        {
            Debug.Log("Purchase Failed");
        }
        return PurchaseProcessingResult.Complete;
    }










    //**************************** Dont worry about these methods ***********************************
    private void Awake()
    {
        TestSingleton();
    }

    void Start()
    {
        if (m_StoreController == null) { InitializePurchasing(); }
    }

    private void TestSingleton()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) => {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}