using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : ICurrency
{
    private int _amount;
    int ICurrency.Amount {
        get
        {
            return _amount;
        }
    }

    public string CurrencyCode => "Diamond";

    public void Deposite(int value)
    {
        _amount += value;
    }

    public void Withdraw(int value)
    {
        _amount -= value;
    }
    public void Save()
    {
        PlayerPrefs.SetInt(CurrencyCode, _amount);
    }
    public void Load()
    {
        if(PlayerPrefs.HasKey(CurrencyCode))
            _amount = PlayerPrefs.GetInt(CurrencyCode);
        else
            _amount = 0;
    }
}
