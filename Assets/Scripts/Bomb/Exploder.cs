using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRange = 2.5f;
    [SerializeField] private float _explosionPower = 1f;

    public void Explode(GameObject bomb)
    {
        var position = bomb.transform.position;
        var hits = Physics.SphereCastAll(position, _explosionRange, Vector3.one, _explosionRange);
        float scaledRange = _explosionRange / bomb.transform.localScale.magnitude;
        float scaledPower = _explosionPower / bomb.transform.localScale.magnitude;

        foreach (var hit in hits)
        {
            if (hit.rigidbody == null)
                continue;

            if (hit.transform.gameObject == bomb.gameObject)
                continue;

            var positionDifference = (hit.transform.position - position);
            var direction = positionDifference.normalized;
            var rangedPower = scaledPower / (positionDifference.magnitude / scaledRange);
    
            if(hit.rigidbody)
                hit.rigidbody.velocity = direction * rangedPower;
        }
    }
}
