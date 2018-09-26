﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre : Peca
{

	public Torre(Jogador j) : base(j)
	{
	}

	public override List<Movimento> ListaMovimentos(Tabuleiro tabuleiro, Casa origem)
	{
		var movimentos = new List<Movimento>();

		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, +1, 0));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, -1, 0));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, 0, +1));
		movimentos.AddRange(Movimento.SeguindoDirecao(tabuleiro, origem, 0, -1));

		return movimentos;
	}

}
