using UnityEngine;

public class OutlineKeeper : MonoBehaviour
{
    private PlayerReferences playerReferences;
    private Outline outlineRef;

    private void Start()
    {
        outlineRef = transform.GetComponent<Outline>();
    }
    void Update()
    {
        TrackDistance();
        //Rotate();
    }

    public void SetReferences(PlayerReferences references)
    {
        playerReferences = references;
    }

    private void TrackDistance()
    {
        float currentDistance = Mathf.Abs(Vector3.Distance(playerReferences.GetPlayerTransform().position, transform.position));
        float distanceInverted = (1 / currentDistance) * 25;
        float distanceSmoothed = Mathf.Round(distanceInverted * 10.0f) * 0.1f;

        //Debug.Log(distanceSmoothed);

        if (distanceSmoothed >= 50f)
        {
            distanceSmoothed = 7f;
        }

        if (distanceSmoothed <= 12f)
        {
            distanceSmoothed = 0f;
        }

        outlineRef.OutlineWidth = distanceSmoothed / 3;
    }
    private void Rotate()
    {
        transform.Rotate(0, 120 * Time.deltaTime, 0);
    }
}
