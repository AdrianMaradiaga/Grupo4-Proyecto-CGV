using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_and_Damage : MonoBehaviour
{
    public int vida = 100;
    public bool invencible = false;

    public float tiempo_Invencible = 1f;
    public float tiempoFrenado = 0.2f;

    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void RestarVida(int cantidad)
    {
        if (!invencible && vida > 0)
        {
            vida -= cantidad;
            anim.Play("Damage");
            StartCoroutine(Invulnerabilidad());
            StartCoroutine(FrenarVelocidad());

            if (vida == 0)
            {
                GameOver();
            }
        }

        void GameOver()
        {
            Debug.Log("GAME OVER!!!");
            Time.timeScale = 0;
        }


        IEnumerator Invulnerabilidad()
        {
            invencible = true;
            yield return new WaitForSeconds(tiempo_Invencible);
            invencible = false;
        }
        IEnumerator FrenarVelocidad()
        {
            var velocidadActual = GetComponent<PlayerController>().playerSpeed;
            GetComponent<PlayerController>().playerSpeed = 0;
            yield return new WaitForSeconds(tiempoFrenado);
            GetComponent<PlayerController>().playerSpeed = velocidadActual;

        }


    }

}

