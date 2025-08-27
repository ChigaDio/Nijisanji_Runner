using AddressableSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TestLoad : MonoBehaviour
{
    AddressableData<GameObject> data;
    public void Start()
    {
        //ÉeÉXÉgì«Ç›çûÇ›
        data = new AddressableData<GameObject>(GroupCategory.Title,AssetCategory.Prefab);
        data.LoadAsync("Character/Belumond/Prefab/Belumond.prefab",(result) =>
        {

            var obj = GameObject.Instantiate(result.gameObject);
            obj.transform.position.WithVec3Y(-152.72f); 
        }).Forget();

    }

    public void OnDestroy()
    {
        data.Release();
    }
}
