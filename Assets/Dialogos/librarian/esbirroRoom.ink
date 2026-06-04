VAR contador_conversaciones = 0

-> inicio_bibliotecaria_2

=== inicio_bibliotecaria_2 ===
#canvas:Fantasy
// 1. Prioridad: Escenario específico (Fija)
~ temp r = RANDOM(1, 4)
{
    - r == 1: -> conv_1
    - r == 2: -> conv_2
    - r == 3: -> conv_3
    - else: -> conv_4
}

=== conv_1 ===
#speaker:Fantasma #icon:fantasma_confundido
¿Crees que debería preocuparme por esa cosa?

#speaker:Bibliotecaria #icon:librarian_normal
¿La que te sigue a todas partes?

#speaker:Fantasma #icon:fantasma_normal
Sí.

#speaker:Bibliotecaria #icon:librarian_ironica
No.

#speaker:Fantasma #icon:fantasma_feliz
¿Ves? Yo también lo pensaba.

#speaker:Bibliotecaria #icon:librarian_normal
Porque si quisiera matarte ya lo habría hecho.

O es suficiente inutil para no haberlo hecho aún, o tu eres más útil de lo esperado.

#speaker:Fantasma #icon:fantasma_cansado
Tú como siempre animando.

-> END

=== conv_2 ===
#speaker:Fantasma #icon:fantasma_normal
Creo que le caigo bien.

#speaker:Bibliotecaria #icon:librarian_normal
Te dispara constantemente.

#speaker:Fantasma #icon:fantasma_confundido
Pero me sigue a todas partes.

#speaker:Bibliotecaria #icon:librarian_ironica
También las pulgas.

#speaker:Fantasma #icon:fantasma_cansado
Eso ha sido innecesario.

#speaker:Bibliotecaria #icon:librarian_ironica
¿Prefieres que le llame piojo?

#speaker:Fantasma #icon:fantasma_cansado
... 

#speaker:Bibliotecaria #icon:librarian_pesada
Eso pensaba.

-> END

=== conv_3 ===
#speaker:Fantasma #icon:fantasma_pensativo
¿Crees que esa criatura es inteligente?

#speaker:Bibliotecaria #icon:librarian_normal
Depende.

#speaker:Fantasma #icon:fantasma_confundido
¿De qué?

#speaker:Bibliotecaria #icon:librarian_ironica
De con quién la compares.

#speaker:Fantasma #icon:fantasma_cansado
...

#speaker:Bibliotecaria #icon:librarian_normal
Si la comparamos contigo, sí.

-> END

=== conv_4 ===
#speaker:Fantasma #icon:fantasma_normal
Parece que se ha encariñado conmigo.

#speaker:Bibliotecaria #icon:librarian_normal
Te sigue porque eres el único que le hace parecer listo.

#speaker:Fantasma #icon:fantasma_cansado
Eso duele.

#speaker:Bibliotecaria #icon:librarian_ironica
Entonces has entendido el comentario. 

#icon:librarian_pesada
->END
No eres tan tonto como pensaba.