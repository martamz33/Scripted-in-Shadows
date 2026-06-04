VAR escenario = "ConqueredKingdom"
VAR contador_conversaciones = 0

-> inicio_bibliotecaria

=== inicio_bibliotecaria ===
#canvas:Fantasy
// 1. Prioridad: Escenario específico (Fija)
{
    - escenario == "ConqueredKingdom": 
        -> conversacion_fija_conquered
    - else:
        -> selector_aleatorio
}

=== conversacion_fija_conquered ===
#speaker:Fantasma #icon:fantasma_confundido
¿Qué ocurrió aquí?

#speaker:Bibliotecaria #icon:librarian_normal
Una mujer consiguió todo lo que quería.

#speaker:Fantasma #icon:fantasma_pensativo
No parece que acabara bien.

#speaker:Bibliotecaria #icon:librarian_normal
Nunca suele acabar bien.

Míralo.

#speaker:Fantasma #icon:fantasma_cansado
Todo está destruido.

#speaker:Bibliotecaria #icon:librarian_normal
Exacto.

Algunos dejan monumentos.

Otros dejan ruinas.

#speaker:Fantasma #icon:fantasma_pensativo
¿Y ella?

#speaker:Bibliotecaria #icon:librarian_normal
Una historia triste con demasiados cadáveres. Y estrellas.
-> END

=== selector_aleatorio ===
~ temp r = RANDOM(1, 10)
{
    - r == 1: -> conv_1
    - r == 2: -> conv_2
    - r == 3: -> conv_3
    - r == 4: -> conv_4
    - r == 5: -> conv_5
    - r == 6: -> conv_6
    - r == 7: -> conv_7
    - r == 8: -> conv_8
    - r == 9: -> conv_9
    - else:   -> conv_10
}

=== conv_1 ===
#speaker:Fantasma #icon:fantasma_pensativo
¿Crees que llegaré lejos?

#speaker:Bibliotecaria #icon:librarian_bromista
¿Quieres una respuesta sincera?

#speaker:Fantasma #icon:fantasma_cansado
Nunca es buena señal cuando preguntas eso.

#speaker:Bibliotecaria #icon:librarian_normal
No tengo ni idea.

#speaker:Fantasma #icon:fantasma_mindBlowing
¿Perdón?

#speaker:Bibliotecaria #icon:librarian_normal
He visto genios rendirse.

#icon:librarian_bromista
Y completos idiotas cambiar el mundo.

Así que deja de hacer preguntas imposibles y vuelve a trabajar.

Venga que la curiosidad no te vuelve buen escritor.

-> END

=== conv_2 ===
#speaker:Fantasma #icon:fantasma_normal

¿Crees que voy mejorando?

#speaker:Bibliotecaria #icon:librarian_normal

Objetivamente sí.

#speaker:Fantasma #icon:fantasma_feliz

¡Bien!

#speaker:Bibliotecaria #icon:librarian_normal

Sigues siendo un desastre.

Pero un desastre ligeramente más potable.

#speaker:Fantasma #icon:fantasma_cansado

Nunca tienes una palabra bonita.

#speaker:Bibliotecaria #icon:librarian_ironica

Prefieres que te sea totalmente sincera y te diga mi opinión sin ningún filtro.

#speaker:Fantasma #icon:fantasma_cansado

Viendolo bien, me lo voy a tomar como un cumplido.

#speaker:Bibliotecaria #icon:librarian_pesada

Eso pensaba

-> END

=== conv_3 ===
#speaker:Fantasma #icon:fantasma_confundido

¿Sabes?

Siempre pensé que ser un héroe sería super guay. 

#speaker:Bibliotecaria #icon:librarian_normal

Nadie es bueno siendo héroe.

#speaker:Fantasma #icon:fantasma_mindBlowing

¿Perdón?

#speaker:Bibliotecaria #icon:bibliotecaria_ironica

Los héroes suelen ser gente con grandes problemas de autoestima y ego.

Y una alarmante falta de instinto de supervivencia.

Los cuales prefieren morir por gente desconocida que vivir por sus seres queridos.

#speaker:Fantasma #icon:fantasma_pensativo

No lo había pensado así.

Eso explica muchas cosas.

#speaker:Bibliotecaria #icon:librarian_normal

Más de las que imaginas.

-> END

=== conv_4 ===
#speaker: Bibliotecaria #icon: librarian_normal
¿Sabes cuál es el problema con los escritores jóvenes?

#speaker: Fantasma #icon: fantasma_confundido
¿La falta de talento?

#speaker: Bibliotecaria #icon: librarian_ironica
A parte de eso.

Que todos creen que son especiales.

#speaker: Fantasma #icon: fantasma_normal
Bueno...

Hay que tener confianza.

#speaker: Bibliotecaria #icon: librarian_normal
La confianza está bien.

El problema es cuando la confianza es todo lo que tienes.

#speaker: Fantasma #icon: fantasma_pensativo
...

#speaker: Bibliotecaria #icon: librarian_pesada
He conocido genios que escribían basura.

Y completos inútiles que acabaron siendo excelentes escritores.

#speaker: Fantasma #icon: fantasma_confundido
Eso no parece muy alentador.

#speaker: Bibliotecaria #icon: librarian_normal
Claro que lo es.

Significa que aún tienes opciones de no acabar siendo un inutil.
-> END

=== conv_5 ===
#speaker:Fantasma #icon:fantasma_normal
¿Cuántos libros has leído?

#speaker:Bibliotecaria #icon:librarian_normal
Más de los que tú vas a escribir.

#speaker:Fantasma #icon:fantasma_cansado
Eso ha sido cruel.

#speaker:Bibliotecaria #icon:librarian_ironica
No.

Cruel sería decirte el número.

-> END


=== conv_6 ===
#speaker:Fantasma #icon:fantasma_confundido
¿Crees que tengo talento?

#speaker:Bibliotecaria #icon:librarian_normal
Qué obsesión tenéis todos con el talento.

#speaker:Fantasma #icon:fantasma_normal
¿Entonces no importa?

#speaker:Bibliotecaria #icon:librarian_normal
Importa.

Pero trabajar suele ganar.

Y es una noticia terrible para los vagos.

#speaker:Fantasma #icon:fantasma_cansado
Sabía que acabarías insultándome.

#speaker:Bibliotecaria #icon:librarian_ironica
¿Entonces eres un vago?

#speaker:Fantasma #icon:fantasma_cansado
...

#icon:fantasma_enojado
Calla.

-> END
=== conv_7 ===

#speaker:Fantasma #icon:fantasma_normal
¿Te caigo bien?

#speaker:Bibliotecaria #icon:librarian_normal
A veces.

#speaker:Fantasma #icon:fantasma_feliz
¿A veces?

#speaker:Bibliotecaria #icon:librarian_ironica
Cuando no hablas.

#speaker:Fantasma #icon:fantasma_cansado
Era una trampa.

#speaker:Bibliotecaria #icon:librarian_normal
Sí.

-> END
=== conv_8 ===

#speaker:Fantasma #icon:fantasma_pensativo
¿Alguna vez has conocido a un escritor perfecto?

#speaker:Bibliotecaria #icon:librarian_normal
No.

#speaker:Fantasma #icon:fantasma_normal
Eso es reconfortante.

#speaker:Bibliotecaria #icon:librarian_ironica
Aunque todos creían que lo eran.

Eso los hice triunfar o fracasar.

#speaker:Fantasma #icon:fantasma_cansado
Ah.

Otra vez sin ayudar a mi autoestima.

-> END
=== conv_9 ===
#speaker:Fantasma #icon:fantasma_normal
Hoy he trabajado mucho.

#speaker:Bibliotecaria #icon:librarian_normal
Bien.

#speaker:Fantasma #icon:fantasma_confundido
¿Ya está?

#speaker:Bibliotecaria #icon:librarian_ironica
¿Qué quieres? ¿Una medalla?

#speaker:Fantasma #icon:fantasma_cansado
Un poco de reconocimiento.

#speaker:Bibliotecaria #icon:librarian_pesada
Reconozco que has hecho algo.

Ya está.

#icon:librarian_ironica
¿Feliz?

#speaker:Fantasma #icon:fantasma_cansado
No

-> END

=== conv_10 ===

#speaker:Fantasma #icon:fantasma_confundido
¿Cómo sabes tanto?

#speaker:Bibliotecaria #icon:librarian_normal
Porque escucho.

#speaker:Fantasma #icon:fantasma_normal
Eso parece fácil.

#speaker:Bibliotecaria #icon:librarian_ironica
Lo es.

#icon:librarian_normal
Por eso me sorprende que tan poca gente lo haga.

#speaker:Fantasma #icon:fantasma_cansado
Otra indirecta.

#speaker:Bibliotecaria #icon:librarian_normal
Ni siquiera era indirecta.

-> END