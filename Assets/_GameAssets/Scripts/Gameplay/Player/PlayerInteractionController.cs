using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Consts.WheatTypes.GOLD_WHEAT))
        {
            if (other.gameObject.TryGetComponent<GoldWheatCollectible>(out GoldWheatCollectible gold))
            {
                gold.CollectGoldWheat();
            }
        }

        if (other.CompareTag(Consts.WheatTypes.HOLY_WHEAT))
        {
            if (other.gameObject.TryGetComponent<HolyWheatCollectible>(out HolyWheatCollectible holy))
            {
                holy.CollectHolyWheat();
            }
        }

        if (other.CompareTag(Consts.WheatTypes.ROTTEN_WHEAT))
        {
            if (other.gameObject.TryGetComponent<RottenWheatCollectible>(out RottenWheatCollectible rotten))
            {
                rotten.CollectRottenWheat();
            }
        }
    }
}
