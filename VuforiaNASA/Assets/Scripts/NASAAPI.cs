using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

///<summary>
///Llamados de API de la NASA para recibir texturas de locaciones especificas.
///</summary>
public class NASAAPI : MonoBehaviour
{
    [SerializeField] private GameObject medellin;
    [SerializeField] private GameObject bogota;
    [SerializeField] private GameObject cali;
    [SerializeField] private GameObject cartagena;

    //Referencias a las paginas de cada lugar
    private readonly string NASAMedellin = "https://api.nasa.gov/planetary/earth/assets?lon=-75.58851123655096&lat=6.156493921233248&date=2022-01-01&&dim=0.15&api_key=rWU28f4srI0ABIRcp1dCduyp5NVSB1PMWkFgPYid";
    private readonly string NASABogota = "https://api.nasa.gov/planetary/earth/assets?lon=-74.07545245764365&lat=4.711792107934514&date=2022-01-01&&dim=0.15&api_key=rWU28f4srI0ABIRcp1dCduyp5NVSB1PMWkFgPYid";
    private readonly string NASACali = "https://api.nasa.gov/planetary/earth/assets?lon=-76.53197618488718&lat=3.452470965490156&date=2022-01-01&&dim=0.15&api_key=rWU28f4srI0ABIRcp1dCduyp5NVSB1PMWkFgPYid";
    private readonly string NASACartagena = "https://api.nasa.gov/planetary/earth/assets?lon=-75.48318061309408&lat=10.393461329043493&date=2022-01-01&&dim=0.15&api_key=rWU28f4srI0ABIRcp1dCduyp5NVSB1PMWkFgPYid";

    private void Start() {
        StartCoroutine(GetImageFromWeb());
    }

    ///<summary>
    ///Hace un llamado de la API e inserta la textura en los modelos correspondientes.
    ///</summary>
    private IEnumerator GetImageFromWeb(){
        //Coger el componente MeshRender de cada objeto
        MeshRenderer texturaMedellin = medellin.GetComponent<MeshRenderer>();
        MeshRenderer texturaBogota = bogota.GetComponent<MeshRenderer>();
        MeshRenderer texturaCali = cali.GetComponent<MeshRenderer>();
        MeshRenderer texturaCartagena = cartagena.GetComponent<MeshRenderer>();

        //Crear objetos de tipo UnityWebRequest para poder buscar los URL previamente establecidos
        UnityWebRequest medellinWebInfo = UnityWebRequest.Get(NASAMedellin);
        UnityWebRequest bogotaWebInfo = UnityWebRequest.Get(NASABogota);
        UnityWebRequest caliWebInfo = UnityWebRequest.Get(NASACali);
        UnityWebRequest cartagenaWebInfo = UnityWebRequest.Get(NASACartagena);

        yield return medellinWebInfo.SendWebRequest();
        yield return bogotaWebInfo.SendWebRequest();
        yield return caliWebInfo.SendWebRequest();
        yield return cartagenaWebInfo.SendWebRequest();

        //Verificar si hay errores con la conexion o con la pagina
        if(medellinWebInfo.result == UnityWebRequest.Result.ConnectionError || medellinWebInfo.result == UnityWebRequest.Result.ProtocolError){
            Debug.LogError(medellinWebInfo.error);
        }
        if(bogotaWebInfo.result == UnityWebRequest.Result.ConnectionError || bogotaWebInfo.result == UnityWebRequest.Result.ProtocolError){
            Debug.LogError(bogotaWebInfo.error);
        }
        if(caliWebInfo.result == UnityWebRequest.Result.ConnectionError || caliWebInfo.result == UnityWebRequest.Result.ProtocolError){
            Debug.LogError(caliWebInfo.error);
        }
        if(cartagenaWebInfo.result == UnityWebRequest.Result.ConnectionError || cartagenaWebInfo.result == UnityWebRequest.Result.ProtocolError){
            Debug.LogError(cartagenaWebInfo.error);
        }

        //Guardar todo el archivo .json que envie la pagina
        JSONNode medellinParameters = JSON.Parse(medellinWebInfo.downloadHandler.text);
        JSONNode bogotaParameters = JSON.Parse(bogotaWebInfo.downloadHandler.text);
        JSONNode caliParameters = JSON.Parse(caliWebInfo.downloadHandler.text);
        JSONNode cartagenaParameters = JSON.Parse(cartagenaWebInfo.downloadHandler.text);

        //Guardar el parametro url de cada uno
        string medellinSprite = medellinParameters["url"];
        string bogotaSprite = bogotaParameters["url"];
        string caliSprite = caliParameters["url"];
        string cartagenaSprite = cartagenaParameters["url"];

        //Guardar las texturas del .json
        UnityWebRequest medellinTexture = UnityWebRequestTexture.GetTexture(medellinSprite);
        UnityWebRequest bogotaTexture = UnityWebRequestTexture.GetTexture(bogotaSprite);
        UnityWebRequest caliTexture = UnityWebRequestTexture.GetTexture(caliSprite);
        UnityWebRequest cartagenaTexture = UnityWebRequestTexture.GetTexture(cartagenaSprite);

        yield return medellinTexture.SendWebRequest();
        yield return bogotaTexture.SendWebRequest();
        yield return caliTexture.SendWebRequest();
        yield return cartagenaTexture.SendWebRequest();

        if(medellinTexture.result == UnityWebRequest.Result.ConnectionError || medellinTexture.result == UnityWebRequest.Result.ProtocolError){
            Debug.LogError(medellinWebInfo.error);
        }
        if(bogotaTexture.result == UnityWebRequest.Result.ConnectionError || bogotaTexture.result == UnityWebRequest.Result.ProtocolError){
            Debug.LogError(bogotaWebInfo.error);
        }
        if(caliTexture.result == UnityWebRequest.Result.ConnectionError || caliTexture.result == UnityWebRequest.Result.ProtocolError){
            Debug.LogError(caliWebInfo.error);
        }
        if(cartagenaTexture.result == UnityWebRequest.Result.ConnectionError || cartagenaTexture.result == UnityWebRequest.Result.ProtocolError){
            Debug.LogError(cartagenaWebInfo.error);
        }

        //Colocar la textura en los objetos
        Texture2D medellinTextureOnPlay = DownloadHandlerTexture.GetContent(medellinTexture);
        texturaMedellin.material.mainTexture = medellinTextureOnPlay;
        Texture2D bogotaTextureOnPlay = DownloadHandlerTexture.GetContent(bogotaTexture);
        texturaBogota.material.mainTexture = bogotaTextureOnPlay;
        Texture2D caliTextureOnPlay = DownloadHandlerTexture.GetContent(caliTexture);
        texturaCali.material.mainTexture = caliTextureOnPlay;
        Texture2D cartagenaTextureOnPlay = DownloadHandlerTexture.GetContent(cartagenaTexture);
        texturaCartagena.material.mainTexture = cartagenaTextureOnPlay;
    }
}
