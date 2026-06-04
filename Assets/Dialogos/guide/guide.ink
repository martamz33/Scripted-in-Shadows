VAR guide_talk_count = 0
VAR boss_wins_guide = 0

->guide_talk

=== guide_talk ===
#canvas:Hall
{
    - guide_talk_count == 0: 
        -> intro_guia
    - guide_talk_count == 1: 
        -> segunda_charla_guia
    - boss_wins_guide > 0: 
        -> post_victoria_boss
    - else:
        -> charla_aleatoria_guia
}

=== intro_guia ===
~ guide_talk_count++
#speaker:Fantasma #icon:fantasma_confundido
¿Qué hago aquí? ¿Quién soy? ¿Cómo he llegado? 

Yo iba a cenar una pizza.

¿Y mi pizza? ¿Quiero saber algo?

#icon:fantasma_cansado
Me estoy estresando. Ayuda 

#speaker:Guia #icon:guide_bored
…

#speaker:Fantasma #icon:fantasma_normal
Vale, he entrado en pánico.
#icon:fantasma_confundido
Perdón, pero en serio… ¿Qué es esto?

#speaker:Guia #icon:guide_normal
Despiertas… y lo primero que haces es hacer preguntas.
Es una buena señal.

No todos los que llegan aquí lo hacen.

#speaker:Fantasma #icon:fantasma_confundido
…
¿Dónde estoy?

#speaker:Guia #icon:guide_normal
Estás en una biblioteca.

#speaker:Fantasma #icon:fantasma_normal
Ya… eso ya lo veo.

#icon:fantasma_pensativo
Pero no parece una biblioteca normal.
Y eso no explica por qué soy un fantasma.
Porque normalmente tengo pies y tal.

#speaker:Guia #icon:guide_sarcastic
No.

No lo es.

Esta biblioteca no guarda libros.
Guarda historias.

#speaker:Fantasma #icon:fantasma_confundido
¿Y no es lo mismo?
O sea, los libros son historias… ¿no?

#speaker:Guia #icon:guide_bored
No exactamente.

#speaker:Guia #icon:guide_normal
Los libros contienen historias.
Pero aquí…

las historias existen por sí mismas.

Respiran.
Cambian.
Se defienden.

#speaker:Fantasma #icon:fantasma_normal
…vale, eso no suena muy tranquilizador.

Suena a algo muy irreal… y bastante agresivo.

#speaker:Fantasma #icon:fantasma_pensativo
Y yo soy bastante pacifista, la verdad.

#speaker:Fantasma #icon:fantasma_confundido
Pero entonces… ¿donde es “aquí”?

#speaker:Guia #icon:guide_normal
En un lugar entre lo que fue escrito…
y lo que aún no ha sido contado.

Un lugar donde terminan las historias olvidadas…
y empiezan las que aún buscan existir.

Tu cuerpo sigue en otro lugar.
Hasta que no reclames tu sitio… no podrá reunirse contigo.

#speaker:Fantasma #icon:fantasma_feliz
Eso suena muy poético…
#icon:fantasma_confundido
pero sigo sin entender nada.

#speaker:Guia #icon:guide_normal
No necesitas entenderlo todo.

Solo necesitas saber una cosa:

no estás aquí por accidente.

#speaker:Fantasma #icon:fantasma_confundido
¿Ah, no?
Porque yo recuerdo ir a hacerme una pizza para cenar…
y luego estar cayendo hacia un vacío inconmensurable.
#icon:fantasma_normal
Y ahora estoy aquí.

#icon:fantasma_pensativo
Eso suena bastante a accidente.
Y también un poco a secuestro, si te soy sincero.

#speaker:Guia #icon:guide_normal
Llegas aquí cuando tu historia… está a punto de comenzar.

Cuando aún queda algo por escribir.

#speaker:Fantasma #icon:fantasma_pensativo
…

¿Entonces estoy como en una especie de coma raro?

#speaker:Guia #icon:guide_normal
Depende de cómo quieras leer tu historia.

#speaker:Fantasma #icon:fantasma_cansado
Eso NO responde nada.

#speaker:Guia #icon:guide_normal
Estás entre capítulos.

#speaker:Fantasma #icon:fantasma_cansado
Genial.
Ni vivo ni muerto.

Estoy… en pausa.
#icon:fantasma_pensativo
Ojalá pudiera hacer esto cuando meto la pata.

#speaker:Guia #icon:guide_normal
Estás en proceso.

#speaker:Fantasma #icon:fantasma_pensativo
Vale… y volviendo a lo de antes.

#icon:fantasma_confundido
¿Por qué soy un fantasma?
Porque normalmente tengo cuerpo y todo eso.

#speaker:Guia #icon:guide_bored
Porque aún no tienes lugar aquí.

#speaker:Fantasma #icon:fantasma_confundido
¿Cómo? Entonces… ¿qué hago aquí?

#speaker:Guia #icon:guide_normal
Tienes una silueta.

Pero no una identidad completa.

#speaker:Fantasma #icon:fantasma_pensativo
…

Siempre me he considerado una persona con bastante personalidad.
#icon:fantasma_cansado
Gracias por desmontarlo en dos frases.
#icon:fantasma_pensativo
Seguro que dices eso, porque no me conoces mucho.
#icon:fantasma_enfadado
Mucha gente dice que una de mis mejores características es mi fuerte carácter.

#speaker:Guia #icon:guide_normal
Es lo que ocurre cuando alguien quiere contar historias…
pero aún no sabe cuál es la suya.

#speaker:Fantasma #icon:fantasma_mindBlowing
…

#icon:fantasma_enfadado
Vale.
Eso ya es meter el dedo en la llaga.

#speaker:Guia #icon:guide_normal
Si quieres salir de aquí…

tendrás que escribirla.

#speaker:Fantasma #icon:fantasma_confundido
¿Escribir?
¿En plan… con boli?

Porque no tengo ni papel ni boli.
¿Eso lo dais aquí o cómo va?

#speaker:Guia #icon:guide_normal
Con decisiones.

Con errores.

Con todo aquello que te niegas a enfrentar.

#speaker:Fantasma #icon:fantasma_cansado
Uf…
eso suena más difícil que pelear con monstruos.

#icon:fantasma_pensativo
Además, ahora que lo pienso, decidir no es mi fuerte, precisamente.

#speaker:Guia #icon:guide_normal
Lo es.

#speaker:Fantasma #icon:fantasma_pensativo
…

Entonces… ¿qué tengo que hacer?
¿Un pacto raro, vender mi alma o algo así?

#speaker:Guia #icon:guide_normal
…
Avanzar.

Aprender.

Y demostrar que tu historia merece ser contada.

#speaker:Fantasma #icon:fantasma_pensativo
¿Y si no lo consigo?
No me creo que sea tan importante.

#speaker:Guia #icon:guide_normal
Entonces te convertirás en una más de las historias que habitan en el olvido.

Olvidada.

Incompleta.

#speaker:Fantasma #icon:fantasma_cansado
…
Vale.

Sin presión.

#speaker:Guia #icon:guide_normal
No estás solo.

#speaker:Fantasma #icon:fantasma_cansado
Eso no tranquiliza tanto como crees.

#speaker:Guia #icon:guide_normal
Otros te observarán.

Algunos te ayudarán.

Otros… te pondrán a prueba.

#speaker:Fantasma #icon:fantasma_confundido
¿Tú ...?

#speaker:Guia #icon:guide_sarcastic
Yo…

solo me aseguro de que sigas avanzando.

#speaker:Fantasma #icon:fantasma_normal
O sea… eres como un tutorial.

#speaker:Guia #icon:guide_normal
…

Si eso te ayuda a entenderlo.

#speaker:Fantasma #icon:fantasma_feliz
Perfecto, me encantan los tutoriales.

Siempre me los salto… pero me encantan.

#speaker:Guia #icon:guide_normal
Lo sé.

Por eso estás aquí.

// --- Lógica de fin de conversación e inicio de run ---

#speaker:Guia #icon:guide_normal
Bueno, elige entre estas habilidades, te dotarán de poderes para llegar a tu destino. #OPEN_ABILITY

#speaker:Guia #icon:guide_normal
Perfecto, ve directo a enfrentar a tu destino. #START_RUN

-> END

=== segunda_charla_guia ===
~ guide_talk_count++
#speaker:Fantasma #icon:fantasma_enojado
¡¿Qué demonios ha sido eso?!

#speaker:Guia #icon:guide_normal
Has regresado.

#speaker:Fantasma #icon:fantasma_enojado
¡No me digas que he regresado!

¡Me han matado!

¡He sentido cómo me mataban!

Nunca me habían matado, no se ni como sentirme con eso.

#speaker:Guia #icon:guide_normal
Lo sé.

#speaker:Fantasma #icon:fantasma_confundido
...

Vale.

¿Por qué dices eso como si fuera algo normal?

#speaker:Guia #icon:guide_normal
Porque aquí lo es.

#speaker:Fantasma #icon:fantasma_enojado
Pues en mi mundo no.

En mi mundo la gente se muere una vez y ya está.

Fin.

Se acabó.

No hay segunda ronda.

No hay retorno de la muerte.

#speaker:Guia #icon:guide_normal
Este lugar funciona de otra forma.

#speaker:Fantasma #icon:fantasma_enojado
¡Pues vaya sitio horrible!

¿Dónde me has traído?

Yo no pedí venir aquí.

No pedí pelear contra monstruos.

No pedí morir.

#icon:fantasma_cansado
Yo solo quería cenar una pizza.

#speaker:Guia #icon:guide_normal
Lo sé.

#speaker:Fantasma #icon:fantasma_cansado
No, no lo sabes.

Porque si lo supieras me mandarías de vuelta.

#speaker:Guia #icon:guide_normal
Y si pudiera hacerlo...

¿Crees que ya lo habría hecho?

#speaker:Fantasma #icon:fantasma_pensativo
...

#speaker:Guia #icon:guide_normal
No estoy aquí para retenerte.

#speaker:Fantasma #icon:fantasma_confundido
Pues alguien lo está haciendo.

#speaker:Guia #icon:guide_normal
La historia aún no te ha soltado.

#speaker:Fantasma #icon:fantasma_cansado
Otra vez con las historias.

Siempre las historias.

¿Y qué pasa con lo que yo quiero?

#speaker:Guia #icon:guide_normal
Todavía no lo sabes.

#speaker:Fantasma #icon:fantasma_enojado
¡Claro que lo sé!

Quiero irme a casa.

#speaker:Guia #icon:guide_bored
No.

#icon:guide_normal
Eso es adónde quieres ir.

No es lo que quieres.

#speaker:Fantasma #icon:fantasma_pensativo
...

#speaker:Guia #icon:guide_normal
La muerte duele.

El fracaso también.

#speaker:Fantasma #icon:fantasma_cansado
Gracias por la información.

Lo había notado.

#speaker:Guia #icon:guide_normal
Y aun así...

has regresado.

#speaker:Fantasma #icon:fantasma_confundido
No porque quisiera.

#speaker:Guia #icon:guide_normal
No.

Pero estás aquí.

#speaker:Fantasma #icon:fantasma_pensativo
...

#speaker:Guia #icon:guide_normal
La pregunta ya no es por qué has caído.

La pregunta es qué harás ahora.

#speaker:Fantasma #icon:fantasma_cansado
...

Odio este sitio.

#speaker:Guia #icon:guide_normal
Lo sé.

#speaker:Fantasma #icon:fantasma_enojado
Y tus respuestas también.

#speaker:Guia #icon:guide_normal
Lo sé.

#speaker:Fantasma #icon:fantasma_cansado
...

Uf.

Qué conversación más frustrante.

#speaker:Guia #icon:guide_normal
Bueno, elige una habilidad, que tu destino te espera. #OPEN_ABILITY

#speaker:Guia #icon:guide_normal
Perfecto, ve directo a enfrentar a tu destino. #START_RUN

-> END

=== post_victoria_boss ===
#speaker:Fantasma #icon:fantasma_feliz
Bueno.

Pues resulta que sí podía hacerlo.

#speaker:Guia #icon:guide_normal
Sí.

#speaker:Fantasma #icon:fantasma_normal
No sé por qué, pero esperaba una reacción un poco más impresionante.

#speaker:Guia #icon:guide_normal
¿Como cuál?

#speaker:Fantasma #icon:fantasma_feliz
No sé.

¿Aplausos?

¿Una fiesta?

¿Un "tenías razón desde el principio"?

#speaker:Guia #icon:guide_sarcastic
Nunca dije que no pudieras hacerlo.

#speaker:Fantasma #icon:fantasma_confundido
Tampoco dijiste que pudiera.

#speaker:Guia #icon:guide_bored
Porque necesitabas descubrirlo tú.

#speaker:Fantasma #icon:fantasma_cansado
Claro.

La respuesta de la Guía.

Misteriosa, poco útil y increíblemente frustante.

#speaker:Guia #icon:guide_normal
Y aun así sigues viniendo a hablar conmigo.

#speaker:Fantasma #icon:fantasma_pensativo
...

Touché.

#speaker:Guia #icon:guide_normal
¿Te sientes diferente?

#speaker:Fantasma #icon:fantasma_confundido
¿Después de enfrentarme a monstruos, morir varias veces y derrotar a un héroe con problemas emocionales gigantes?

Sí.

Diría que un poco.

#speaker:Guia #icon:guide_normal
No me refiero a eso.

#speaker:Fantasma #icon:fantasma_pensativo
...

#speaker:Guia #icon:guide_misteriosa
Cuando llegaste aquí...

lo primero que preguntaste fue quién eras.

#speaker:Fantasma #icon:fantasma_normal
Bueno.

Es una pregunta bastante importante.

#speaker:Guia #icon:guide_normal
Y todavía no tienes una respuesta.

#speaker:Fantasma #icon:fantasma_confundido
Gracias por recordármelo.

#speaker:Guia #icon:guide_misteriosa
Pero ahora sigues avanzando incluso sin tenerla.

#speaker:Fantasma #icon:fantasma_pensativo
...

#speaker:Guia #icon:guide_normal
Antes querías escapar.

Ahora quieres entender.

#speaker:Fantasma #icon:fantasma_normal
No exageremos.

Sigo quiero escapar un poco.

#speaker:Guia #icon:guide_sarcastic
…

#speaker:Fantasma #icon:fantasma_cansado
Bueno, bastante.

#speaker:Guia #icon:guide_normal
Lo sé.

#speaker:Fantasma #icon:fantasma_cansado
Qué manía tienes de saber cosas.

#speaker:Guia #icon:guide_bored
Forma parte del trabajo.

#speaker:Fantasma #icon:fantasma_pensativo
...

Entonces...

¿voy por buen camino?

#speaker:Guia #icon:guide_normal
Sigues caminando.

#speaker:Fantasma #icon:fantasma_cansado
Eso no responde a la pregunta.

#speaker:Guia #icon:guide_normal
Lo sé.

#speaker:Fantasma #icon:fantasma_enojado
De verdad que a veces eres desesperante.

#speaker:Guia #icon:guide_misteriosa
Y aun así...

has llegado más lejos de lo que creías posible.

#speaker:Fantasma #icon:fantasma_pensativo
...

#speaker:Fantasma #icon:fantasma_normal
Supongo que eso sí me lo creo.

#speaker:Guia #icon:guide_normal
Entonces descansa.

La siguiente historia te espera.

#speaker:Fantasma #icon:fantasma_cansado
Uf.

Odio cuando dices cosas que suenan importantes.

#speaker:Guia #icon:guide_normal
Lo sé.

#speaker:Guia #icon:guide_normal
Bueno, elige entre estas habilidades, te dotarán de poderes para llegar a tu destino. #OPEN_ABILITY

#speaker:Guia #icon:guide_normal
Perfecto, ve directo a enfrentar a tu destino. #START_RUN

-> END

=== charla_aleatoria_guia ===
~ temp r = RANDOM(1, 5)
{
    - r == 1: -> guia_random_A
    - r == 2: -> guia_random_B
    - r == 3: -> guia_random_C
    - r == 4: -> guia_random_D
    - else: -> guia_random_E
}
-> END

=== guia_random_A ===
#speaker:Guia #icon:guide_normal
Has cambiado.

#speaker:Fantasma #icon:fantasma_confundido
¿Ya estamos otra vez con eso?

Porque sigo teniendo el mismo aspecto.
Bueno.

Más o menos.

#speaker:Guia #icon:guide_bored
No hablo de eso.

#speaker:Fantasma #icon:fantasma_pensativo
Menos mal.

Porque si me estoy transformando físicamente agradecería que alguien me avisara.

Y que fuera a mi antiguo ser, que era precioso.

#speaker:Guia #icon:guide_normal
Las personas cambian poco a poco.

Tan poco...

que rara vez lo notan.

#speaker:Fantasma #icon:fantasma_normal
Bueno.

Yo sí noto que ahora duermo peor.

Tengo más ansiedad, estoy más cansado, y tengo más crisis existenciales.

#speaker:Guia #icon:guide_normal
Y aun así sigues avanzando.

#speaker:Fantasma #icon:fantasma_cansado
Eso empieza a sonar sospechosamente parecido a un cumplido.

#speaker:Guia #icon:guide_normal
Quizá lo sea. Bueno, elige entre estas habilidades, te dotarán de poderes para llegar a tu destino. #OPEN_ABILITY

#speaker:Guia #icon:guide_normal
Perfecto, ve directo a enfrentar a tu destino. #START_RUN

-> END
=== guia_random_B ===
#speaker:Fantasma #icon:fantasma_confundido
Oye.

Pregunta.

¿Hay alguien que limpie este sitio?

#speaker:Guia #icon:guide_normal
¿Qué quieres decir?

#speaker:Fantasma #icon:fantasma_normal
Hay libros por todas partes.

Escaleras imposibles.

Puertas que aparecen de la nada.

Esto parece diseñado por alguien con mucho talento...

y muy poca supervisión.

#icon:fantasma_confundido
Hogwards se inspiro en esta biblioteca, porque vamos ambos parecen laberintos.

#speaker:Guia #icon:guide_bored
La biblioteca cambia constantemente.

#speaker:Fantasma #icon:fantasma_pensativo
Sí, eso no responde a mi pregunta.

#speaker:Guia #icon:guide_normal
No necesita ser limpiada.

#speaker:Fantasma #icon:fantasma_cansado
Claro.

Porque cuando los libros son mágicos la higiene deja de existir.
#speaker:Guia #icon:guide_normal
El polvo no llega a este plano. Bueno, elige entre estas habilidades, te dotarán de poderes para llegar a tu destino. #OPEN_ABILITY

#speaker:Guia #icon:guide_normal
Perfecto, ve directo a enfrentar a tu destino. #START_RUN

-> END
=== guia_random_C ===
#speaker:Fantasma #icon:fantasma_confundido
Oye.

¿Hay más gente como yo?

#speaker:Guia #icon:guide_normal
Sí.

#speaker:Fantasma #icon:fantasma_normal
¿Y dónde están?

#speaker:Guia #icon:guide_sarcastic
Escribiendo.

#speaker:Fantasma #icon:fantasma_confundido
Vale.

Eso ha sido sospechosamente ambiguo.

#speaker:Guia #icon:guide_normal
Algunos siguen recorriendo historias.

Otros ya encontraron la suya.

#speaker:Fantasma #icon:fantasma_pensativo
Y ninguno se pasa por aquí a tomar un café o algo.

#speaker:Guia #icon:guide_bored
No tenemos café.

#speaker:Fantasma #icon:fantasma_mindBlowing
Vale.

Ahora sí que quiero irme.

Y, ¿chocolate?

#speaker:Guia #icon:guide_bored
No tenemos tampoco.

#speaker:Fantasma #icon:fantasma_mindBlowing
Vale.

Ahora sí definitivamente quiero irme.

#speaker:Guia #icon:guide_normal
Eso no es posible. Bueno, elige entre estas habilidades, te dotarán de poderes para llegar a tu destino. #OPEN_ABILITY

#speaker:Guia #icon:guide_normal
Perfecto, ve directo a enfrentar a tu destino. #START_RUN

-> END

=== guia_random_D ===
#speaker:Fantasma #icon:fantasma_normal
¿Sabes?

Este sitio es raro.

#speaker:Guia #icon:guide_normal
Lo sé.

#speaker:Fantasma #icon:fantasma_normal
No, quiero decir raro de verdad.

Hay momentos en los que parece...

demasiado silencioso.

#speaker:Guia #icon:guide_bored
Las historias escuchan.

#speaker:Fantasma #icon:fantasma_confundido
No.

No.

No me gusta esa frase.

#speaker:Guia #icon:guide_normal
¿Por qué?

#speaker:Fantasma #icon:fantasma_cansado
Porque implica que los libros tienen opiniones sobre mí.

Y no necesito más juicios sobre mi vida.

#speaker:Guia #icon:guide_normal
Las tienen. Bueno, elige entre estas habilidades, te dotarán de poderes para llegar a tu destino. #OPEN_ABILITY

#speaker:Guia #icon:guide_normal
Perfecto, ve directo a enfrentar a tu destino. #START_RUN

-> END

=== guia_random_E ===
#speaker:Fantasma #icon:fantasma_pensativo
Echo de menos cosas rarísimas.

#speaker:Guia #icon:guide_normal
¿Cómo cuáles?

#speaker:Fantasma #icon:fantasma_normal
Mi cama.

Internet.

La pizza.

No necesariamente en ese orden.

#speaker:Guia #icon:guide_normal
Eso significa que aún recuerdas quién eras.

#speaker:Fantasma #icon:fantasma_confundido
¿Eso es bueno?

#speaker:Guia #icon:guide_normal
Sí.

Mientras sigas recordándolo.

#speaker:Fantasma #icon:fantasma_pensativo
...

Eso ha sonado bastante más inquietante de lo que esperaba.

#speaker:Guia #icon:guide_normal
Lo es. Bueno, elige entre estas habilidades, te dotarán de poderes para llegar a tu destino. #OPEN_ABILITY

#speaker:Guia #icon:guide_normal
Perfecto, ve directo a enfrentar a tu destino. #START_RUN

-> END
