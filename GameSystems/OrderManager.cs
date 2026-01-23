using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class OrderManager : MonoBehaviour
{
    
    [Header("- List of Orders")]
    public List<OrderData> OrderList = new List<OrderData>();
    [Header("- List of Customers")]
    public List<GameObject> CustomerList = new List<GameObject>();
    [Header("- List of Order Times")]
    public List<float> OrderTimeList = new List<float>();



    void UpdateOrderTime()
    {
        for(int i = 0; i < OrderTimeList.Count; i++)
        {
            OrderTimeList[i] = CustomerList[i].GetComponent<NpcOrderWaittingState>().UpdateWaitingTime();
        }
    }

    // Update is called once per frame
    public void GetOrder(OrderData orderData, GameObject customer)
    {
        OrderList.Add(orderData);
        CustomerList.Add(customer);
        OrderTimeList.Add(0f);
    }

    public void RemoveOrder(GameObject customer, OrderData orderData)
    {
        int index = CustomerList.IndexOf(customer);

        OrderList.RemoveAt(index);
        CustomerList.RemoveAt(index);
        OrderTimeList.RemoveAt(index);
    }
}
