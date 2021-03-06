﻿using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;

public class PartidaTestes {



                                                        
    public static void removeintervalo(Tabuleiro t, int x, int y, int linhajogador, int colunajogador,int modo)
    {
        
        
        int iniciox, inicioy;
        int fimx,fimy,dif;
        if(x > linhajogador)
        {
            iniciox = linhajogador;
            fimx = x;
        } 
        else
        {
            iniciox = x;
            fimx = linhajogador;
        }

        if(y > colunajogador)
        {
            inicioy = colunajogador;
            fimy = y;
        } 
        else
        {
            inicioy = y;
            fimy = colunajogador;
        }
        if(modo == 1)
        {
          //  Debug.Log("???????????????????????");
            dif = Math.Abs(linhajogador-x);
            if(dif != Math.Abs(y-colunajogador) )
            {
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA FUDEU");
            }
            iniciox = iniciox +1;
            inicioy = inicioy +1;
            // isso é para evitar remover as peças que queremos usar na simulação
            while(iniciox < fimx  && inicioy < fimy)
            {
              //  Debug.Log("removendo...");
              //  Debug.Log(inicioy);
              //  Debug.Log(iniciox);
                if(t.tabuleiro[inicioy,iniciox].EstaOcupada())
                {
                    t.tabuleiro[inicioy,iniciox].PopPeca();
                }
                iniciox = iniciox +1;
                inicioy = inicioy +1;
            }   
        }
        else
        {
             // deve-se remover o intervalo entre essa peça e o rei horizontalmente e verticalmente
            for(int i=iniciox+1; i < fimx ;i++)
            {
                 if(t.tabuleiro[colunajogador,i].EstaOcupada())
                 {
                    t.tabuleiro[colunajogador,i].PopPeca();
                 }
            }

            for(int i=inicioy+1; i < fimy ;i++)
            {
                if(t.tabuleiro[i,linhajogador].EstaOcupada())
                 {
                    t.tabuleiro[i,linhajogador].PopPeca();
                 }       
            }
        
        }

        
       
    }

    public static void pegaEocupa(Tabuleiro t, int x, int y , int poslinha , int codpeca)
    {
        if(x == 0 && y == 0)
        {
            Debug.Log("entendido não considerarei...");
            return;
        } 
        Casa pe = t.tabuleiro[codpeca,poslinha];
        Peca e = pe.PopPeca();
        if(t.tabuleiro[y,x].EstaOcupada()) 
        {
                t.tabuleiro[y,x].PopPeca();
        }
        if(codpeca == 3 && e is Rainha )
        {
                    Debug.Log("ok queen");
        }
        if((codpeca == 0 || codpeca == 7) && e is Torre )
        {
                    Debug.Log("ok torre");
        }
        if((codpeca == 2 || codpeca == 5) && e is Bispo )
        {
                    Debug.Log("ok bispo");
        }
        t.tabuleiro[y,x].ColocarPeca(e);
    }

    public static void criasituacao(Partida p, int x1, int y1, int x2, int y2, int x3, int y3, int linhajogador)
    {
           
            int colunajogador = 4; // posição do rei na sua linha
            Tabuleiro t = p.Tabuleiro;
            int poslinha = Math.Abs(linhajogador -7); // vai ser 7 ou 0 (serve para pegar a peça inimiga)
            
            removeintervalo(t,x1,y1,linhajogador,colunajogador,0);
            removeintervalo(t,x2,y2,linhajogador,colunajogador,1);
            removeintervalo(t,x3,y3,linhajogador,colunajogador,0);

            pegaEocupa(t,x1,y1,poslinha,3);
            pegaEocupa(t,x2,y2,poslinha,2);
            pegaEocupa(t,x3,y3,poslinha,0);

            
            
            
            
           
            
    }
    // note que passar false e algum atributo garante uma assertion incorreta.
    // note que passar true e não passar pelo menos 2 posições para xeque mate torna a assertion incorreta.
    [TestCase(false,0,0,0,0,0,0,0,false)] // esse caso teste é o caso "passe direto" logo vai falhar  ( ou seja o rei não está nem em xeque)  
    [TestCase(true,0,0,0,3,7,0,6,false)]  // caso do rei encurrlado
    [TestCase(false,0,3,4,3,7,0,0,false)]  // caso que o rei pode escapar 

    [TestCase(true,0,0,0,3,7,0,6,true)]  // caso do rei encurrlado e tem integração
    [TestCase(false,0,3,4,3,7,0,0,true)]  // caso que o rei pode escapar e tem integração
    public void TesteVitoria(bool acaba, int linhajogador, int x1, int y1, int x2, int y2, int x3, int y3, bool integra)
    {
       Partida p = new Partida();
       if(acaba && (x1+x2+x3+y1+y2+y3 != 0))
       {
          criasituacao(p, x1, y1, x2, y2, x3, y3, linhajogador); // função usada para cercar o rei com até 3 diferentes peças (rainha, bispo , torre e simular)
       }
       
       Tabuleiro t = p.Tabuleiro;
       
       // testando se o problema é a simulação...
  //      Debug.Log(t.tabuleiro[4,3].PecaAtual is Rainha);
  //   Debug.Log(t.tabuleiro[7,3].PecaAtual is Bispo);  
   //   Debug.Log(t.tabuleiro[6,0].PecaAtual is Torre);

  //    Debug.Log(t.tabuleiro[4,1].EstaOcupada());  
  //  Debug.Log(t.tabuleiro[4,0].PecaAtual.jDono.Cor);

    //    Debug.Log(t.tabuleiro[5,1].EstaOcupada());
  //      Debug.Log(t.tabuleiro[6,2].EstaOcupada());

  //    Debug.Log(t.tabuleiro[0,3].EstaOcupada());
  //    Debug.Log(t.tabuleiro[4,0].PecaAtual.jDono.Cor);
  //    Debug.Log(t.tabuleiro[7,3].PecaAtual.jDono.Cor);
  //    Debug.Log(t.tabuleiro[6,0].PecaAtual.jDono.Cor);
       if(integra)
       {
          Debug.Log("Entendido, vou usar a chamada de passar a vez");
          p.PassarAVez();
          p.PassarAVez();
          Assert.AreEqual(acaba,p.fim);
       }
       else
       {
          

          p.TurnoAtual = 2;
          if(acaba && (x1+x2+x3+y1+y2+y3 != 0))
          {
              
              
              Debug.Log("O JOGO TERMINOU");
              Assert.AreEqual(acaba,p.VerificaVitoria());
              
              
          
          }
          else
          {
              Debug.Log("O JOGO NAO TERMINOU");
              Assert.AreEqual(acaba,p.VerificaVitoria());
          }
       
        }
           


    }




}
