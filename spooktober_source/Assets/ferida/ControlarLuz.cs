using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlarLuz : MonoBehaviour
{

    public Light _luz;
    public bool _working = true; //Quando não tiver energia na casa, essa variável deve ser falsa
    public bool _estado = false;
    private bool _colidindo = false; //Controla se o player está ou não no range do interruptor

    void Update()
    {
        if (_colidindo && Input.GetKeyDown(KeyCode.E) && _working)
        {      
            _estado = !_estado;
            AcenderApagar(_estado);
        }
        if(!_working)
        {
            _luz.enabled = false;
        }
    }
    
    void AcenderApagar(bool _acao)
    {
        _luz.enabled = _acao;
    }

    private void OnTriggerEnter(Collider _col)
    {
        if (_col.gameObject.tag == "Player")
        {
            _colidindo = true;
        }
    }

    private void OnTriggerExit(Collider _col)
    {
        if (_col.gameObject.tag == "Player")
        {
            _colidindo = false;
        }
    }
}