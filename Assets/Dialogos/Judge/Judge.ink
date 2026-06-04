VAR boss_wins = 0

=== intro_judgement ===
#canvas:Judgement
{
    - boss_wins == 0:
        -> intro_primera_vez
    - boss_wins == 1:
        -> intro_segunda_vez
    - else:
        -> intro_aleatoria_avanzada
}

=== intro_primera_vez ===
#speaker:Fantasma #icon:fantasma_confundido
…

Vale.

Pregunta rápida.

¿Quién eres?

Y otra más importante…

¿Por qué siento que me estás juzgando muchísimo ahora mismo?

#speaker:Juez #icon:juez_serious
Porque lo estoy haciendo.

#speaker:Fantasma #icon:fantasma_mindBlowing
AH. Vale. Perfecto.

#icon:fantasma_normal
Nada traumático entonces.

Tú tranquilo.
Yo nervioso.

#speaker:Juez #icon:juez_normal
Has demostrado ser resistente

Has caído.

Has regresado.

Y aun así… continúas avanzando.

Eso demuestra que no eres tan decepcionante como pareces.

#speaker:Fantasma #icon:fantasma_cansado
También he sufrido muchísimo, por si eso suma puntos.

#speaker:Juez #icon:juez_serious
El sufrimiento no tiene mérito.

Sobrevivir tampoco.

#speaker:Fantasma #icon:fantasma_pensativo
…

Vale.

Eso ha sonado bastante duro.

#speaker:Juez #icon:juez_normal
El sufrimiento no hace especial a nadie.

Lo que decides hacer con él... sí

#speaker:Fantasma #icon:fantasma_confundido
Vale, entonces…

¿Quién eres exactamente?

#icon:fantasma_normal
Porque la Guía daba vibras raras.

Pero tú directamente pareces el jefe final de mis traumas.

#speaker:Juez #icon:juez_serious
Yo observo el final de las historias.

Y decido cuáles merecen continuar.

Las que no, son olvidadas.

#speaker:Fantasma #icon:fantasma_cansado
Perfecto.

O sea que eres literalmente una crítica literaria con depresión.

#speaker:Juez #icon:juez_normal
He condenado historias por menos.

#speaker:Fantasma #icon:fantasma_mindBlowing
…

Vale.

Eso sí ha dado miedo.

#speaker:Juez #icon:juez_serious
Muchos llegan aquí buscando poder.

Otros reconocimiento.

Tú llegaste huyendo.

#speaker:Fantasma #icon:fantasma_confundido
Perdona, ¿huyendo de qué?

#speaker:Juez #icon:juez_normal
De ti mismo.

#speaker:Fantasma #icon:fantasma_pensativo
…

Eso me lo voy a tomar a lo personal.

#speaker:Juez #icon:juez_serious
Las personas no temen al dolor.

Temen mirarse demasiado tiempo y reconocerlo en ellos.

#speaker:Fantasma #icon:fantasma_normal
Bueno, técnicamente yo quería irme a casa.

#speaker:Juez #icon:juez_normal
No.

Querías dejar de sufrir.

No es lo mismo.

#speaker:Fantasma #icon:fantasma_pensativo
…

#speaker:Juez #icon:juez_serious
Y aun así seguiste avanzando.

Eso es lo único que ha impedido que desaparezcas.

Y que te considere una pérdida de tiempo.

#speaker:Fantasma #icon:fantasma_confundido
Vale, necesito saber una cosa.

¿Esto es un juicio de verdad?

En plan…

¿me vais a condenar?

¿Lanzarme un rayo místico?

#icon:fantasma_normal2
Porque aviso que mentalmente ya era inestable.

Y toda esta aventura de la muerte no esta ayudando a que mejore.

#speaker:Juez #icon:juez_normal
Si hubiera decidido destruirte…

ya no existirías. 

#speaker:Fantasma #icon:fantasma_mindBlowing
…

Eso ha dado muchísimo más miedo que el rayo.

#speaker:Juez #icon:juez_serious
Cada decisión revela algo.

Cada fracaso también.

Cuando dejaste de poder ganar…

mostraste quién eras realmente.

#speaker:Fantasma #icon:fantasma_pensativo
Entonces…

¿todo esto era para ver qué tipo de persona soy?

#speaker:Juez #icon:juez_normal
No.

Era para obligarte a descubrirlo.

#speaker:Fantasma #icon:fantasma_pensativo
…

#speaker:Juez #icon:juez_serious
La mayoría jamás lo consigue.

Viven convencidos de entenderse.

Mueren siendo desconocidos para sí mismos.

#speaker:Fantasma #icon:fantasma_cansado
Jo.

¿No serás tu la alegría de la huerta, no?

#speaker:Juez #icon:juez_normal
No estoy aquí para darte consolarte.

Estoy aquí para juzgarte a ti y a tu historia.

#speaker:Fantasma #icon:fantasma_confundido
¿Y si mi historia no merece ser recordada?

#speaker:Juez #icon:juez_serious
Entonces desaparecerá.

Como miles antes que tú.

#speaker:Fantasma #icon:fantasma_pensativo
…

#speaker:Juez #icon:juez_normal
Así que responde.

Después de todo lo que has vivido…

¿todavía deseas continuar?

#speaker:Fantasma #icon:fantasma_cansado
…

No lo sé.

Pero creo que todavía no quiero que termine.

#speaker:Juez #icon:juez_serious
He oído mejores respuestas.

#speaker:Fantasma #icon:fantasma_mindBlowing
¿PERDÓN?

#speaker:Juez #icon:juez_normal
Por suerte para ti…

nunca me han interesado las respuestas correctas.

#speaker:Fantasma #icon:fantasma_pensativo
…

#speaker:Juez #icon:juez_castigo
Vamos a ver si tu historia merece sobrevivir
#START_BOSS
-> END

=== intro_segunda_vez ===
#speaker:Fantasma #icon:fantasma_cansado
…

Vale.

He vuelto.

¿Contento?

#speaker:Juez #icon:juez_serious
No.

#speaker:Fantasma #icon:fantasma_cansado
Jo, pues empezamos bien.

#speaker:Juez #icon:juez_normal
Has regresado demasiado rápido.

#speaker:Fantasma #icon:fantasma_normal
Bueno, tampoco es como si estar muerto fuera mi pasatiempo favorito.

#speaker:Juez #icon:juez_serious
El problema no es que hayas vuelto.

Es que sigues siendo el mismo.

#speaker:Fantasma #icon:fantasma_pensativo
…

Vale.

#icon:fantasma_pensativo
Directo al ego, empezamos bien.

#speaker:Juez #icon:juez_normal
Sigues entrando aquí esperando sobrevivir.

No vencer.

#speaker:Fantasma #icon:fantasma_cansado
Perdona por querer conservar mis pocas piezas en su sitio.

#speaker:Juez #icon:juez_serious
El miedo sigue decidiendo por ti.

#speaker:Fantasma #icon:fantasma_cansado
Bueno, técnicamente escapar es una estrategia tan válida cómo cualquiera.

Muchos animales lo hacen.

#speaker:Juez #icon:juez_normal
Tú no eres un animal.

Eres alguien desesperado por convencerse de que es lo suficientemente especial para continuar.

#speaker:Fantasma #icon:fantasma_pensativo
…

#icon:fantasma_cansado
Genial. Otro ataque personal.

#speaker:Juez #icon:juez_serious
Porque lo es.

#speaker:Fantasma #icon:fantasma_enfadado
Pues yo sí creo que merezco continuar.

He pasado por muchísimo para llegar aquí.

#speaker:Juez #icon:juez_normal
Y aun así…

sigues siendo igual al resto.

#speaker:Fantasma #icon:fantasma_confundido
¿Y qué se supone que quieres de mí entonces?

¿Que deje de tener miedo?

Porque eso no funciona así.

#speaker:Juez #icon:juez_serious
No.

Quiero ver qué puedes a pesar de él.

#speaker:Fantasma #icon:fantasma_pensativo
…

#speaker:Juez #icon:juez_normal
Cualquiera puede avanzar cuando cree que va a ganar.

Pero tú…

Aún con la incertidumbre has regresado.

#speaker:Fantasma #icon:fantasma_normal
Bueno…

sí que da un poco de rabia perder.

#speaker:Juez #icon:juez_serious
Eso no es orgullo.

Todavía no.

Pero quizá…

pueda convertirse en algo útil.

#speaker:Fantasma #icon:fantasma_confundido
Guau.

Creo que eso es lo más cercano a un cumplido que me has dicho nunca.

#speaker:Juez #icon:juez_normal
No confundas expectativas con admiración.

Aún no me has mostrado nada nuevo.

#speaker:Fantasma #icon:fantasma_cansado
Uf…

Estas pesadito hoy, ¿eh?.

#speaker:Juez #icon:juez_serious
Y aun así sigues viniendo.

#speaker:Fantasma #icon:fantasma_pensativo
…

Touché.

Aunque no es como si tuviera muchas opciones.

#speaker:Juez #icon:juez_castigo
Entonces vuelve a intentarlo.

A ver si esta vez no te rindes antes de empezar.
#START_BOSS
-> END

=== intro_aleatoria_avanzada ===
~ temp r = RANDOM(1, 3)
{
    - r == 1: -> intro_avanzada_A
    - r == 2: -> intro_avanzada_B
    - else:   -> intro_avanzada_C
}
-> END

=== intro_avanzada_A ===
#speaker:Fantasma #icon:fantasma_cansado
...

Otra vez.

#speaker:Juez #icon:juez_serious
Sí.

#speaker:Fantasma #icon:fantasma_confundido
¿Sabes?

Empiezo a odiar esta habitación.

#speaker:Juez #icon:juez_castigo
Y aun así siempre vuelves a morir en ella.

#START_BOSS
-> END

=== intro_avanzada_B ===
#speaker:Fantasma #icon:fantasma_cansado
Bueno. Aquí estamos otra vez.

#speaker:Juez #icon:juez_serious
Sí.

#speaker:Fantasma #icon:fantasma_confundido
¿Alguna frase amenazante antes de empezar?

#speaker:Juez #icon:juez_normal
Ya conoces mis respuestas.

#speaker:Fantasma #icon:fantasma_normal
Justo por eso preguntaba.

#START_BOSS
-> END

=== intro_avanzada_C ===
#speaker:Fantasma #icon:fantasma_normal
¿Y si esta vez ganamos los dos?

#speaker:Juez #icon:juez_serious
Eso no funciona así.

#speaker:Fantasma #icon:fantasma_confundido
¿Seguro?

Porque empiezo a pensar que te gusta pelear conmigo.

#speaker:Juez #icon:juez_serious
Empiezo a pensar que hablas demasiado.

#speaker:Fantasma #icon:fantasma_feliz
Eso es un sí.

#START_BOSS
-> END

=== post_pelea_ganada ===
{
    - boss_wins == 0: -> post_sin_ganar
    - boss_wins == 1: -> post_primera_victoria
    - boss_wins == 2: -> post_segunda_victoria
    - boss_wins == 3: -> post_tercera_victoria
    - boss_wins == 4: -> post_cuarta_victoria
    - else: -> post_general
}
-> END

=== post_sin_ganar ===
#speaker:Fantasma #icon:fantasma_cansado
…

#speaker:Juez #icon:juez_serious
Eso era todo.

#speaker:Fantasma #icon:fantasma_confundido
Perdona…

#icon:fantasma_enojado
¿“Eso era todo”?

Casi me matas.

#speaker:Juez #icon:juez_normal
Casi no.

#speaker:Fantasma #icon:fantasma_pensativo
…

Vale.

Sigue siendo muy incómodo hablar contigo.

#speaker:Juez #icon:juez_serious
Esperaba más.

#speaker:Fantasma #icon:fantasma_enojado
Bueno, perdón por no ganar contra el ser más traumático que he visto en mi vida.

#speaker:Juez #icon:juez_normal
No perdiste por falta de fuerza.

Perdiste porque te quebraste.

#speaker:Fantasma #icon:fantasma_confundido
¿Perdona?

#speaker:Juez #icon:juez_serious
En cuanto entendiste que podías morir…

dejaste de avanzar.

#speaker:Fantasma #icon:fantasma_enojado
¡Pues claro!

¡Tú dabas muchísimo miedo!

#speaker:Juez #icon:juez_normal
Veo que eres como el resto.

#speaker:Fantasma #icon:fantasma_pensativo
…

#speaker:Juez #icon:juez_serious
Dudaste.

Retrocediste.

Suplicaste sobrevivir.

#speaker:Fantasma #icon:fantasma_cansado
…

Eso ha dolido un poquito, eh.

#speaker:Juez #icon:juez_normal
Las historias mediocres siempre terminan igual.

Creen ser especiales…

hasta que algo les demuestra que no lo son.

#speaker:Fantasma #icon:fantasma_confundido
Vale, pero yo he llegado hasta aquí.

#icon:fantasma_cansado
He pasado por monstruos, héroes corruptos, traumas varios…

Algo tendré, ¿no?

#speaker:Juez #icon:juez_serious
¿Te crees distinto?

Todos los que se han enfrentado a mi han pasado por lo mismo.

Todos tenéis potencial.

#speaker:Fantasma #icon:fantasma_normal
Ah.

Bueno.

Eso suena bien.

#speaker:Juez #icon:juez_normal
Por lo menos lo tenías.

#speaker:Fantasma #icon:fantasma_mindBlowing
¿CÓMO QUE “LO TENÍA”?

#speaker:Juez #icon:juez_serious
Veo que me equivoque.

#speaker:Fantasma #icon:fantasma_pensativo
…

#speaker:Juez #icon:juez_normal

Porque cuando llegó el momento de enfrentarte a algo superior a ti…

te convertiste en alguien pequeño.

Como todos.

#speaker:Fantasma #icon:fantasma_cansado
Jo.

Qué forma tan elegante de destrozarme emocionalmente.

#speaker:Juez #icon:juez_serious
No he terminado de juzgarte.

#speaker:Fantasma #icon:fantasma_confundido
¿Entonces por qué sigo aquí?

#speaker:Juez #icon:juez_normal
No he acabado de evaluarte.

Ahora vete.

Todavía no eres digno de este juicio.

-> END

=== post_primera_victoria
#speaker:Fantasma #icon:fantasma_cansado
…

¿He ganado?

#speaker:Juez #icon:juez_serious
No.

#speaker:Fantasma #icon:fantasma_confundido
¿Cómo que “no”?

¡Pero si sigues tú ahí tirado con dramatismo de jefe final!

#speaker:Juez #icon:juez_normal
Has sobrevivido al juicio.

No lo has superado.

#speaker:Fantasma #icon:fantasma_cansado
Uf…

Contigo todo tiene letra pequeña.

#speaker:Juez #icon:juez_serious
Y aun así…

#icon:juez_pride
has llegado más lejos que la mayoría.

#speaker:Fantasma #icon:fantasma_pensativo
…

Vale.

Eso sí ha sonado un poco a cumplido.

Creo.

#speaker:Juez #icon:juez_serious
No confundas reconocimiento con aprobación.

#icon:juez_normal

Todavía eres imperfecto.

Todavía dudas.

Todavía temes.

#speaker:Fantasma #icon:fantasma_normal
Bueno, perdón por ser una persona medianamente funcional y tener miedo de morir.

#speaker:Juez #icon:juez_serious
No fue el miedo lo que te permitió vencerme.

Fue que dejaste de obedecerlo.

#speaker:Fantasma #icon:fantasma_pensativo
…

#speaker:Juez #icon:juez_normal
Por primera vez…

actuaste porque lo decidiste tú.

No porque esperabas sobrevivir.

No porque querías escapar.

Simplemente avanzaste.

#speaker:Fantasma #icon:fantasma_confundido
…espera.

Eso suena sospechosamente profundo.

#speaker:Juez #icon:juez_serious
Las cadenas más difíciles de ver…

son aquellas que uno confunde con su propia voluntad.

#speaker:Fantasma #icon:fantasma_pensativo
…

Esto me va a dejar con una crisis existencial.

Me lo veo venir.

#speaker:Juez #icon:juez_normal
Buena señal.

#speaker:Fantasma #icon:fantasma_normal
Qué raro eres.

#speaker:Juez #icon:juez_serious
Hasta ahora…

cada paso que dabas estaba condicionado.

Por miedo.

Por culpa.

Por desesperación.

#speaker:Fantasma #icon:fantasma_confundido
Bueno, dicho así parezco un desastre emocional con patas.

#speaker:Juez #icon:juez_normal
Lo eras.

#speaker:Fantasma #icon:fantasma_enojado
OYE.

#speaker:Juez #icon:juez_serious
Pero durante un instante…

fuiste libre.

#speaker:Fantasma #icon:fantasma_pensativo
…

#speaker:Juez #icon:juez_normal
Y eso…

es algo que este lugar ya casi había olvidado.

#speaker:Fantasma #icon:fantasma_confundido
Vale, ahora eres tú el que está dando vibras raras.

#speaker:Juez #icon:juez_sombra
Este mundo está construido sobre historias.

Las historias tienen reglas.

Roles.

Finales.

Todo existe para cumplir aquello que fue escrito.

#speaker:Fantasma #icon:fantasma_pensativo
…

#speaker:Juez #icon:juez_normal
Pero tú…

por un momento…

actuaste fuera de ello.

#speaker:Fantasma #icon:fantasma_confundido
¿Eso es malo?

#speaker:Juez #icon:juez_serious
…

No.

#speaker:Fantasma #icon:fantasma_pensativo
Ah.

Pues sinceramente esperaba muchísimo un discurso de “la libertad es peligrosa” o algo así.

#speaker:Juez #icon:juez_normal
La libertad no es peligrosa.

Es difícil.

La mayoría prefiere obedecer algo.

Aunque les destruya.

#speaker:Fantasma #icon:fantasma_pensativo
…

Eso ha sonado demasiado real.

#speaker:Juez #icon:juez_serious
Por eso sigo juzgándote.

Porque todavía no sé qué harás con ella.

#speaker:Fantasma #icon:fantasma_confundido
Entonces… ¿ahora qué?

#speaker:Juez #icon:juez_normal
Ahora continúas.

#speaker:Fantasma #icon:fantasma_cansado
¿Sin premio?
¿Sin discurso épico?
¿Sin espada legendaria?

#speaker:Juez #icon:juez_sombra
Ya has obtenido algo más raro.

#speaker:Fantasma #icon:fantasma_confundido
…¿Trauma?

#speaker:Juez #icon:juez_normal
Que no te vea igual que al resto.

#speaker:Fantasma #icon:fantasma_mindBlowing
…

Espera.

¿Eso era posible?

#speaker:Juez #icon:juez_serious
No hagas que me arrepienta.

#speaker:Fantasma #icon:fantasma_normal
Vale, sí.

Definitivamente eso ha sido un cumplido.

#speaker:Juez #icon:juez_normal
No.

Ha sido una advertencia.

No vuelvas a convertirte en alguien mediocre.

-> END

=== post_segunda_victoria ===
#speaker:Fantasma #icon:fantasma_cansado
…

Vale.

Voy a decirlo.

Pegarle a una manifestación física de mis traumas está empezando a cansarme muchísimo.

#icon:fantasma_pensativo
Aun así en cierto grado es hasta satisfactorio.

#speaker:Juez #icon:juez_serious
Y aun así sigues haciéndolo.

#speaker:Fantasma #icon:fantasma_confundido
Bueno.

Tú tampoco pareces especialmente interesado en dejar de intentar matarme.

#speaker:Juez #icon:juez_normal
Porque sigues sin entenderlo.

#speaker:Fantasma #icon:fantasma_pensativo
…

#icon:fantasma_confundido
¿El qué?

#speaker:Juez #icon:juez_serious
Crees que ganar una vez significa algo.

Crees que resistir te hace distinto.

Que te hace especial.

#speaker:Fantasma #icon:fantasma_enfadado
Bueno, perdona por pensar que derrotarte tenía algo de mérito.

Porque sinceramente eres bastante insoportable.

Y no es como si fueras algo fácil de matar, ¿sabías?

#speaker:Juez #icon:juez_normal
No me derrotaste.

Sobreviviste lo suficiente.

#speaker:Fantasma #icon:fantasma_confundido
Eso ha sonado muy a excusa para alguien que estaba estampado contra el suelo hace cinco minutos.

#speaker:Juez #icon:juez_serious
Y aun así dudabas.

Seguiste teniendo miedo.

#speaker:Fantasma #icon:fantasma_normal
Bueno.

Sí.

Tú das muchísimo miedo.

Creo que eso es una reacción totalmente normal.

#speaker:Juez #icon:juez_normal
El miedo no es el problema.

Lo es obedecerlo.

#speaker:Fantasma #icon:fantasma_pensativo
…

#speaker:Juez #icon:juez_serious
Durante el combate seguías esperando el momento exacto para rendirte.

Lo vi en cada decisión.

#speaker:Fantasma #icon:fantasma_enfadado
Pues no me rendí.

#speaker:Juez #icon:juez_normal
No.

Y eso es precisamente lo que empieza a molestarme.

#speaker:Fantasma #icon:fantasma_confundido
…

Vale.

Eso ha sonado sospechosamente personal.

#speaker:Juez #icon:juez_serious
Las personas siempre terminan igual.

El dolor las rompe.

El miedo las controla.

Y acaban aceptando el lugar miserable que les toca.

#speaker:Fantasma #icon:fantasma_normal
Uf.

Pues igual necesitas conocer gente menos deprimente.

#speaker:Juez #icon:juez_normal
No hablo de ellos.

Hablo de ti.

#speaker:Fantasma #icon:fantasma_pensativo
…

#speaker:Juez #icon:juez_serious
Y aun así…

sigues tomando decisiones que no encajan con alguien derrotado.

#speaker:Fantasma #icon:fantasma_confundido
¿Eso era un cumplido?

#speaker:Juez #icon:juez_normal
No.

Era una advertencia.

-> END

=== post_tercera_victoria ===
#speaker:Fantasma #icon:fantasma_cansado
…

¿Sabes qué es lo peor?

Que ya me estoy acostumbrando a esto.

Y eso me preocupa muchísimo.

#speaker:Juez #icon:juez_serious
Eso debería preocuparte.

#speaker:Fantasma #icon:fantasma_normal
Mira el lado bueno.

Por lo menos ya no entro aquí llorando internamente.

#speaker:Juez #icon:juez_normal
Ahora entras desafiante.

Es peor.

#speaker:Fantasma #icon:fantasma_confundido
Perdón por desarrollar autoestima, supongo.

#speaker:Juez #icon:juez_serious
No confundas determinación con arrogancia.

#speaker:Fantasma #icon:fantasma_enfadado
Y tú no confundas miedo con debilidad.

#speaker:Juez #icon:juez_normal
…

#speaker:Fantasma #icon:fantasma_pensativo
…

Vale.

Eso sí te ha molestado.

#speaker:Juez #icon:juez_serious
Sigues sin comprender cuál es tu lugar.

#speaker:Fantasma #icon:fantasma_confundido
¿Y cuál es exactamente?

Porque honestamente parece que quieres que me arrastre por el suelo y te dé la razón.

Mientras nado en el mar de mis lágrimas.

#speaker:Juez #icon:juez_normal
Quiero que aceptes la verdad.

#speaker:Fantasma #icon:fantasma_normal
Uf.

No me gusta como suena eso.

#speaker:Juez #icon:juez_serious
El dolor cambia a las personas.

Siempre.

#speaker:Fantasma #icon:fantasma_pensativo
Sí.

#speaker:Juez #icon:juez_serious
Las rompe.

Las vuelve egoístas.

Cobardes.

Vacías.

#speaker:Fantasma #icon:fantasma_normal
O las vuelve más fuertes.

#speaker:Juez #icon:juez_serious
No.

#speaker:Fantasma #icon:fantasma_enfadado
Pues mírame.

#speaker:Juez #icon:juez_normal
Te estoy mirando.

Y veo a alguien que todavía no entiende lo cerca que está de convertirse en algo miserable.

#speaker:Fantasma #icon:fantasma_pensativo
…

#speaker:Juez #icon:juez_serious
Cada vez que luchas conmigo…

te acercas más al borde.

#speaker:Fantasma #icon:fantasma_normal
Y aun así sigo eligiendo avanzar.

#speaker:Juez #icon:juez_serious
Eso es exactamente lo que no tiene sentido.

#speaker:Fantasma #icon:fantasma_confundido
¿Sabes qué creo?

#speaker:Juez #icon:juez_normal
No especialmente.

#speaker:Fantasma #icon:fantasma_normal
Creo que tú sí cruzaste esa línea.

Y ahora te molesta ver a alguien que no quiere hacerlo.

#speaker:Juez #icon:juez_serious
…

#speaker:Fantasma #icon:fantasma_pensativo
…

#speaker:Juez #icon:juez_normal
Cuidado.

#speaker:Fantasma #icon:fantasma_confundido
¿Eso es una amenaza?

#speaker:Juez #icon:juez_serious
No.

Es una dvertencia.

-> END

=== post_cuarta_victoria ===
#speaker:Fantasma #icon:fantasma_cansado
…

Entonces…

¿ya está?

#speaker:Juez #icon:juez_serious
…

#speaker:Fantasma #icon:fantasma_normal
Vale.

Eso ha sonado muy dramático.

Y normalmente cuando alguien se queda callado después de una pelea…

muere.

#icon:fantasma_confundido

¿Te vas a morir?

#speaker:Juez #icon:juez_normal
Ya no queda nada que probar.

#speaker:Fantasma #icon:fantasma_pensativo
…

#speaker:Juez #icon:juez_serious
Te enfrentaste al miedo.

Al dolor.

A la desesperación.

Y aun así…

seguiste eligiendo quién querías ser.

#speaker:Fantasma #icon:fantasma_normal
Bueno…

dicho así suena hasta bonito.

#speaker:Juez #icon:juez_normal
No.

Suena imposible.

#speaker:Fantasma #icon:fantasma_confundido
…

#speaker:Juez #icon:juez_serious
Eso es lo que me derrotó.

No tu fuerza.

No tus victorias.

Tu libertad.

#speaker:Fantasma #icon:fantasma_pensativo
…

#speaker:Juez #icon:juez_normal
Yo renuncié a ella hace mucho tiempo.

Convencido de que el sufrimiento acababa convirtiendo a todos en algo miserable.

#speaker:Juez #icon:juez_serious
Pero tú…

seguiste avanzando sin convertirte en alguien vacío.

#speaker:Fantasma #icon:fantasma_normal
Vacío seguro que no...

#icon_fantasma_pensativo

Tengo ansiedad y bastantes problemas, la verdad.

Más que vacío diría inestable.

#speaker:Juez #icon:juez_pride
Y aun así seguiste siendo tú.

#speaker:Fantasma #icon:fantasma_pensativo
…

#speaker:Juez #icon:juez_pride
Eso…

es lo que hace digna a una historia.

#speaker:Fantasma #icon:fantasma_confundido
Entonces…

¿he aprobado el trauma-examen?

#speaker:Juez #icon:juez_normal
No.

Has demostrado que mereces continuar escribiendo.

#speaker:Fantasma #icon:fantasma_pensativo
…

#speaker:Juez #icon:juez_serious
La Biblioteca no recuerda a los más fuertes.

Recuerda a aquellos capaces de cambiar…

sin perder quiénes eran.

#speaker:Fantasma #icon:fantasma_normal
…

Jo.

#icon:fantasma_feliz
Eso ha sonado sospechosamente a cumplido.

#speaker:Juez #icon:juez_normal
No te acostumbres.

#speaker:Fantasma #icon:fantasma_feliz
Yes.

#speaker:Juez #icon:juez_serious
Ya no eres un visitante.

#speaker:Fantasma #icon:fantasma_pensativo
…

#speaker:Juez #icon:juez_normal
Ahora…

formas parte de estas historias.

#speaker:Fantasma #icon:fantasma_mindBlowing
…

Espera.

¿Eso significa que tengo una estantería?

#speaker:Juez #icon:juez_serious
Sí.

La tendrás.

#speaker:Fantasma #icon:fantasma_normal
…

Vale.

Eso es probablemente lo más feliz que he estado desde que empezó todo esto.

#speaker:Juez #icon:juez_normal
Entonces ve.

Escribe algo digno de permanecer aquí.

#speaker:Fantasma #icon:fantasma_pensativo
…

Lo haré.

#speaker:Juez #icon:juez_pride
Lo sé.

-> END

=== post_general ===
#speaker:Fantasma #icon:fantasma_cansado
...

Bueno.

Eso ha sido bastante humillante.

#speaker:Juez #icon:juez_normal
Sí.

#speaker:Fantasma #icon:fantasma_normal
¿Sabes?

Antes cuando decías cosas así daban miedo.

Ahora solo suenan un poco borde.

#speaker:Juez #icon:juez_serious
Y aun así has vuelto.

#speaker:Fantasma #icon:fantasma_normal
Sí, bueno.

Resulta que seguir adelante era una de las pocas cosas en las que tenías razón.

#speaker:Juez #icon:juez_serious
...

#speaker:Fantasma #icon:fantasma_confundido
¿Qué?

#speaker:Juez #icon:juez_normal
Nada.

Todavía me resulta extraño.

#speaker:Fantasma #icon:fantasma_normal
¿El qué?

#speaker:Juez #icon:juez_serious
Ver a alguien romper el ciclo.

#speaker:Fantasma #icon:fantasma_pensativo
Bueno.

Técnicamente muchas palizas, varias crisis existenciales y una cantidad preocupante de traumas.

No fue exactamente gratis.

#speaker:Juez #icon:juez_normal
Nunca lo es.

#speaker:Fantasma #icon:fantasma_confundido
¿Y ahora qué?

¿Me das otro discurso deprimente?

¿Una prueba secreta?

¿Una metáfora rara sobre libros?

#speaker:Juez #icon:juez_normal
No.

#speaker:Fantasma #icon:fantasma_mindBlowing
...

Eso sí que no me lo esperaba.

#speaker:Juez #icon:juez_serious
Ya has demostrado lo que necesitabas demostrar.

#speaker:Fantasma #icon:fantasma_pensativo
...

#speaker:Juez #icon:juez_normal
Caer ya no forma parte de tu juicio.

Solo forma parte de tu historia.

#speaker:Fantasma #icon:fantasma_normal
Eso ha sonado sorprendentemente bonito.

#speaker:Juez #icon:juez_serious
No te acostumbres.

#speaker:Fantasma #icon:fantasma_feliz
Ahí está.

Ese es el juez que conozco.

#speaker:Juez #icon:juez_normal
Levántate.

Todavía quedan páginas por escribir.

#speaker:Fantasma #icon:fantasma_normal
Sí, sí.

Ya voy.

-> END
