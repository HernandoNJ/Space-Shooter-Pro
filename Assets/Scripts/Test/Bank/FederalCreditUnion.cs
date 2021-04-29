using UnityEngine;

[System.Serializable]
public class FederalCreditUnion : Bank
{
    public int availableCashToLend;

    public void ApprovedLending()
    {
        Debug.Log("Loan approved");
        
    }

}
