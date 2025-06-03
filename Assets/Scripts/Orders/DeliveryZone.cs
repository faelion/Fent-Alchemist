using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        AlchemyItem item = other.GetComponent<AlchemyItem>();
        if (item == null)
        {
            Debug.Log($"El objeto {other.gameObject.name} no es un AlchemyItem.");
            return;
        }

        if (NPCManager.Instance.IsItemRequested(item.definition))
        {
            var order = NPCManager.Instance.GetOrderForItem(item.definition);

            // Marcarlo como completado (esto depende de tu lógica)
            order.completed = true;

            // Dar recompensa, destruir ítem, etc.
            Destroy(other.gameObject);
            Debug.Log($"¡Has entregado {order.requestedItem.name} y completado el pedido! +{order.rewardMoney} monedas.");
            order.assignedTo.EndOrder();
            NPCManager.Instance.RemoveUIOrder(order);
        }
        else
        {
            Debug.Log($"El ítem {other.gameObject.name} no está en ningún pedido activo.");
        }
    }
}