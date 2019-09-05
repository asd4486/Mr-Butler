using UnityEngine;
using UnityEngine.UI;

public enum CustomerStatus
{
    NoOrder,
    Ordering,
    Eating
}

public class AICustomer : MonoBehaviour
{
    GameMain main;
    [SerializeField] Animator myAnimator;

    [SerializeField] Transform customerGroup;
    GameObject customerUI;

    Transform cameraTransform;

    [SerializeField] Text exclamationPoint;
    [HideInInspector] public CustomerStatus myStatus;
    [SerializeField] Image waitBar;

    [SerializeField] GameObject customerSprite;
    Material customerMat;

    [SerializeField] ParticleSystem fxStar;

    [SerializeField] Texture2D texNoOrder;
    [SerializeField] Texture2D texOrdering;
    [SerializeField] Texture2D texEating;

    float changeStatusTimer;
    float nextStatusTime;

    private void Awake()
    {
        main = FindObjectOfType<GameMain>();

        customerUI = customerGroup.GetComponentInChildren<Canvas>().gameObject;
        customerUI.SetActive(false);

        cameraTransform = Camera.main.transform;

        customerMat = customerSprite.GetComponent<MeshRenderer>().material;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetNextStatusInfos();
    }

    private void Update()
    {
        //customerGroup.LookAt(cameraTransform);
        customerGroup.eulerAngles = new Vector3(0, cameraTransform.eulerAngles.y, 0);

        if (!main.isGameStart) return;

        changeStatusTimer += Time.deltaTime;
        SetOrderTimerFill();

        if (changeStatusTimer > nextStatusTime)
        {
            myStatus = (CustomerStatus)Random.Range(0, 2);
            SetNextStatusInfos();
        }
    }

    void SetOrderTimerFill()
    {
        if (myStatus == CustomerStatus.NoOrder) return;
        waitBar.fillAmount = (nextStatusTime - changeStatusTimer) / nextStatusTime;
    }

    void SetNextStatusInfos()
    {
        changeStatusTimer = 0;
        switch (myStatus)
        {
            case CustomerStatus.NoOrder:
                customerMat.mainTexture = texNoOrder;
                customerUI.SetActive(false);
                nextStatusTime = 2;

                myAnimator.SetTrigger("idle");
                break;
            case CustomerStatus.Ordering:
                customerMat.mainTexture = texOrdering;
                customerUI.SetActive(true);
                exclamationPoint.color = waitBar.color = Color.green;
                nextStatusTime = 5;

                myAnimator.SetTrigger("ordering");
                break;
            case CustomerStatus.Eating:
                customerMat.mainTexture = texEating;
                exclamationPoint.color = waitBar.color = Color.red;
                nextStatusTime = 2;

                myAnimator.SetTrigger("eating");
                break;
        }
    }

    public void OrderComplete()
    {
        myStatus = CustomerStatus.Eating;
        fxStar.Play();
        SetNextStatusInfos();
    }
}
