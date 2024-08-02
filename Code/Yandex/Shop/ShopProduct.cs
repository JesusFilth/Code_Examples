using Agava.YandexGames;
using Reflex.Attributes;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopProduct : MonoBehaviour
{
    [SerializeField] private RawImage _icon;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _coins;
    [SerializeField] private Button _buy;

    private CatalogProduct _product;
    private IPurchase _purchase;

    [Inject] private LocalizationTranslate _localizationTranslate;
    [Inject] private MessageBox _messageBox;
     
    private void OnEnable()
    {
        _buy.onClick.AddListener(OnPurchaseButtonClick);
    }

    private void OnDisable()
    {
        _buy.onClick.RemoveListener(OnPurchaseButtonClick);
    }

    public void Init(CatalogProduct product, IPurchase purchase)
    {
        _purchase = purchase;

        _product = product;
        _price.text = product.price;
        _coins.text = GetCoinCount().ToString();

        if (Uri.IsWellFormedUriString(_product.imageURI, UriKind.Absolute))
            StartCoroutine(DownloadAndSetProductImage(_product.imageURI));
    }

    private IEnumerator DownloadAndSetProductImage(string imageUrl)
    {
        var remoteImage = new RemoteImage(imageUrl);
        remoteImage.Download();

        while (!remoteImage.IsDownloadFinished)
            yield return null;

        if (remoteImage.IsDownloadSuccessful)
            _icon.texture = remoteImage.Texture;
    }

    private void OnPurchaseButtonClick()
    {
        Billing.PurchaseProduct(_product.id, (purchaseProductResponse) =>
        {
            Debug.Log($"Purchased {purchaseProductResponse.purchaseData.productID}");
            _messageBox.Show(_localizationTranslate.GetMessage(LocalizationMessageType.Purchased));
            _purchase.AddCoins(GetCoinCount());
        });
    }

    private int GetCoinCount()
    {
        string[] paths = _product.id.Split('_');
        int coins = int.Parse(paths[1]);

        return coins;
    }
}
