// Variable que modificaremos desde Unity cuando el jugador pulse un botón
VAR poder_elegido = ""

-> conversacion_principal

=== conversacion_principal ===
// Si es la primera vez que entramos a este nudo, vamos a la intro. Si no, a las aleatorias.
{ conversacion_principal == 1:
    -> primera_vez
- else:
    -> conversaciones_aleatorias
}

=== primera_vez ===
#canvas:Fantasy #speaker:Tolkien #icon:tolkien_normal
Bienvenido. Veo que estás en busca de escribir tu propio destino
#icon:tolkien_risa
Jajaja siempre es más fácil escribir historias ajenas a nosotros.
#icon:tolkien_normal
No puedo recorrer tu camino por ti… pero sí puedo ayudarte a entenderlo.
#icon:tolkien_nostalgia
Yo también estuve en tus zapatos... Ayyyy… qué tiempos aquellos.
No siempre fui un aclamado escritor, antes fui un pequeño niño con un sueño:
Ser capaz de adiestrar a las palabras para que formen las más épicas historias.
No necesitas inspiración divina.
Necesitas paciencia… y una pasión que no te deje descansar.
Dices que quieres convertirte en escritor… pero eso no empieza con una decisión. Empieza con una obsesión.
Debes escribir aquello que realmente te importa. Si no sientes la historia… nadie más lo hará.
Voy a darte algo que puede cambiar el rumbo de tu historia.

#speaker:Fantasma #icon:fantasma_mindBlowing
¿¿¿¿COMO???
#icon:fantasma_feliz
Puedes hacer eso, dime que tienes un ascensor mágico que me lleve al centro.
O un anillo teletransportador.

#speaker:Tolkien #icon:tolkien_risa
jajaja... qué imaginación tienes pequeño.
#icon:tolkien_normal
Aunque lamento decepcionarte, no tengo nada de eso.
Aquí no tengo ningún dominio sobre las otras criaturas.

#speaker:Fantasma #icon:fantasma_confundido
Pero... pero ¿por qué, si tú has creado algunas de ellas?

#speaker:Tolkien #icon:tolkien_misterioso
En eso tienes razón, pero míralo así: soy como un padre.
Yo di vida a mis historias para que ellas pudieran vivir sin mí.

#speaker:Fantasma #icon:fantasma_normal2
No lo había pensado así, entonces las historias que escribes son como tus hijos.

#speaker:Tolkien #icon:tolkien_risa
Exacto. Y como con los hijos, no los puedes controlar durante mucho tiempo jajaja.
#icon:tolkien_normal
Lo que te voy a dar es a elegir entre 3 poderes que te ayudarán en tu aventura. #OPEN_UI
-> eleccion_poder


=== conversaciones_aleatorias ===
// El símbolo ~ baraja las opciones para que salgan de forma aleatoria sin repetirse rápido
{~ -> conv1 | -> conv2 | -> conv3 | -> conv4 | -> conv5 | -> conv6 | -> conv7 }

= conv1
#speaker:Tolkien #icon:tolkien_normal
Bienvenido de nuevo, eres como un nuevo libro a escribir.
Porque las historias rara vez las puedes terminar en el primer intento.
#speaker:Fantasma #icon:fantasma_cansado
Jajaja qué gracioso eres, yo quería acabar prontoooo, los bichos son malos conmigo me pegannnn.
Y yo soy pacifista, no soy fan de la violencia. Y he usado más violencia ahora que en toda mi vida.
#speaker:Tolkien #icon:tolkien_risa
Jajajajaj, no puedes apresurarte, las mejores cosas no se consiguen de la noche a la mañana.
#icon:tolkien_normal
Mira con los libros, ¿crees que escribí mis mejores historias en una noche?
#speaker:Fantasma #icon:fantasma_confundido
Voy a suponer que no, mínimo tardarías dos.
#speaker:Tolkien #icon:tolkien_risa
Jajaja, valoro tu optimismo pequeño, pero tardé bastante más.
#icon:tolkien_nostalgia
Escucha, no tengas prisa. Algunas historias necesitan tiempo. A veces años. Incluso décadas.
Yo mismo tardé mucho más de lo que imaginaba en dar forma a mis mundos.
Las historias no se apresuran. Se construyen… igual que los mundos que las sostienen.
Poco a poco ponemos los cimientos que sostienen nuestra historia.
#speaker:Fantasma #icon:fantasma_pensativo
Supongo que escribir no es como un sprint sino una carrera.
#speaker:Tolkien #icon:tolkien_misterioso
Exactamente. Aunque no pueda ayudarte a escribir más rápido...
Sí puedo hacer que continuar tu travesía resulte más fácil. #OPEN_UI
-> eleccion_poder

= conv2
#speaker:Tolkien #icon:tolkien_misterioso
Cada vez que regresas traes contigo una nueva versión de ti mismo.
#speaker:Fantasma #icon:fantasma_cansado
Sí, una versión más cansada de mí mismo traigo.
#speaker:Tolkien #icon:tolkien_misterioso
Aparte de eso, también es una versión más sabia y madura.
#speaker:Fantasma #icon:fantasma_normal
Si tú lo dices será cierto, tú eres el adulto premium, yo solo estoy en el tutorial de la vida.
Aunque no te voy a negar que estoy aprendiendo cosas.
#speaker:Tolkien #icon:tolkien_misterioso
Presta atención a esto.
#icon:tolkien_nostalgia
Escucha a quienes te rodean. Incluso a aquellos que critican tu trabajo.
No para obedecerlos… sino para mejorar aquello que aún no funciona.
Las críticas no tienen que hundirte, tienen que ayudarte a ser mejor.
#speaker:Fantasma #icon:fantasma_cansado
Pero pero hay gente que es muy mala y solo critica por celos.
#speaker:Tolkien #icon:tolkien_misterioso
Tienes que saber diferenciar entre verdaderas críticas y celos.
Nada nace perfecto. Todo puede refinarse.
#speaker:Fantasma #icon:fantasma_pensativo
...
Supongo que tienes razón, todo tiene hueco de mejora.
#speaker:Tolkien #icon:tolkien_normal
Ves, hablando de mejoras, elige una entre estas. #OPEN_UI
-> eleccion_poder

= conv3
#speaker:Tolkien #icon:tolkien_misterioso
Incluso los caminos más oscuros pueden volver a recorrerse… si uno tiene la voluntad de hacerlo.
#speaker:Fantasma #icon:fantasma_normal
Estás tú hoy muy reflexivo Tolkien.
#icon:fantasma_cansado
Dime cómo ponerme en ese estado, para poder tener inspiración. ¡No tengo ideasssss!
#speaker:Tolkien #icon:tolkien_risa
Jajaja, para tener ideas no tienes que estar en un estado en concreto.
#icon:tolkien_misterioso
Algunas de las ideas más poderosas nacen cuando la mente no está intentando crearlas.
A veces aparecen cuando dejas de buscarlas.
#speaker:Fantasma #icon:fantasma_pensativo
No lo había pensado. También te digo que las mejores ideas a mí se me ocurren en el baño, pero nunca tengo boli para apuntarlas.
#speaker:Tolkien #icon:tolkien_risa
JAJAJA, tú siempre tan único pequeño pupilo.
#icon:tolkien_misterioso
No te preocupes, si es una buena idea, hallará la manera de volver a ti.
Como tú también encontrarás tu camino… incluso hasta el juicio.
Aunque incluso el mejor viaje… se vuelve más llevadero con ayuda. #OPEN_UI
-> eleccion_poder

= conv4
#speaker:Tolkien #icon:tolkien_misterioso
Pocos son aquellos capaces de seguir luchando para conseguir sus objetivos.
#speaker:Fantasma #icon:fantasma_timido
Así me haces sentir incluso útil, me voy a sonrojar y todo.
#speaker:Tolkien #icon:tolkien_normal
Todo el mundo tiene algo que merece ser contado. 
La perfección suele ser una ilusión.
A menudo, la perfección no es más que imperfecciones intentando parecer completas.
#speaker:Fantasma #icon:fantasma_normal2
Verdad. Yo pienso lo mismo, conozco a una que siempre da una imagen de perfección.
#icon:fantasma_enojado
Y siempre pienso que es demasiado falsa esa imagen. Además de la pereza que da mantener esa imagen todo el tiempo.
#speaker:Tolkien #icon:tolkien_nostalgia
No todo debe ser perfecto desde el principio.
Incluso las grandes historias comienzan siendo imperfectas.
#icon:tolkien_normal
Dicho eso, vamos a darte un pequeño poder imperfecto que te ayude en tu imperfecta aventura. #OPEN_UI
-> eleccion_poder

= conv5
#speaker:Tolkien #icon:tolkien_normal
Veo que has vuelto pequeño, ¿te has encontrado algo interesante?
#speaker:Fantasma #icon:fantasma_cansado
Ufff… lo más interesante que me he encontrado es una torre que me ha seguido a lo kamikaze.
#icon:fantasma_confundido
En este piso las cosas son muy raras.
#speaker:Tolkien #icon:tolkien_normal
No son raras. Son otra forma de mirar el mundo. 
¿O dirías que las grandes historias lo son?
#speaker:Fantasma #icon:fantasma_normal
A ver muy normales normales, lo que viene a ser normal según la definición de la RAE, no.
Aunque sí que tiene su propio encanto.
#speaker:Tolkien #icon:tolkien_misterioso
Exacto, eso es lo importante. Al fin y al cabo la fantasía es otra forma de entender la realidad.
#speaker:Fantasma #icon:fantasma_pensativo
Ummm… no lo había pensado así.
La fantasía para mí siempre había sido algo mágico e inalcanzable. No lo había visto de esa manera.
#speaker:Tolkien #icon:tolkien_normal
Míralo así a partir de ahora y ya verás cómo cambiarás la forma de ver las historias.
Ahora vamos a cambiar tu forma de enfrentar esta aventura, vamos a mejorarla. #OPEN_UI
-> eleccion_poder

= conv6
#speaker:Tolkien #icon:tolkien_normal
Has regresado a estas páginas. Eso solo puede significar que tu historia aún no está escrita.
#speaker:Fantasma #icon:fantasma_cansado
Supongo que estoy en ello.
Aunque no me va muy bien, estoy todo el rato sobreviviendo más que viviendo.
Por lo menos desde que caí aquí. Quién diría que querer ser escritor me traería a esto.
#speaker:Tolkien #icon:tolkien_misterioso
Bueno, los caminos que nos llevan a nuestro destino son muy diversos.
#icon:tolkien_nostalgia
A veces los caminos que menos nos esperamos nos llevan a las mejores aventuras.
#speaker:Fantasma #icon:fantasma_confundido
Ya, pero cómo sabes que es la ruta correcta. Cómo sabes que no te has equivocado.
Cómo sabes si las historias que creas serán capaces de trascender generaciones y marcar una era.
#speaker:Tolkien #icon:tolkien_nostalgia
Puede que nadie recuerde tu historia.
Pero eso no es lo importante. Importa que tú estés orgulloso de ella.
Porque escribir… no es garantizar la inmortalidad. Es intentar crear algo que merezca existir.
#speaker:Fantasma #icon:fantasma_mindBlowing
Buaaaaaa. Nunca lo había pensado.
#icon:fantasma_feliz
Son cosas que no controlo yo, tengo que crear algo que me haga sentir orgulloso.
Obviamente se podrá mejorar, pero lo importante es crear cosas dando nuestro mayor esfuerzo.
Sintiéndonos orgullosos de lo que somos capaces de hacer.
#speaker:Tolkien #icon:tolkien_nostalgia
Veo que lo vas a entender pequeño, no es importante su impacto… sino lo que significa para ti.
#icon:tolkien_normal
Elige… y veamos qué camino decides seguir. #OPEN_UI
-> eleccion_poder

= conv7
#speaker:Tolkien #icon:tolkien_misterioso
Has vuelto. Eso significa que aún no has terminado tu historia.
#speaker:Fantasma #icon:fantasma_normal2
Eso espero, que aunque soy un fantasma ahora, yo sigo vivo.
#icon:fantasma_enojado
Mi historia no acabará hasta que acaben conmigo. Y hierba mala nunca muere, aunque el mundo lo intente por activa y por pasiva.
#speaker:Tolkien #icon:tolkien_risa
Jasjasj, piensa que no se muere quien se va sino quien se olvida.
#speaker:Fantasma #icon:fantasma_cansado
Ya, pero ahora mismo si me muero la única que no se olvidará de mí es mi mami.
E iba a ir a visitarla cuando caí aquí. Así que no creo que esté muy contenta ella tampoco.
#speaker:Tolkien #icon:tolkien_risa
Jajaja, No te preocupes pequeño, que saldrás de aquí vivo.
#icon:tolkien_normal
No formas parte de la biblioteca aún, por ahora solo eres un visitante.
Tómalo como un viaje donde vienes a aprender.
Cuando te hayas ganado el respeto de estas estanterías podrás volver para reclamar tu lugar en ellas.
Para ello tendrás que relatar grandes historias.
#speaker:Fantasma #icon:fantasma_cansado
Eso por ahora lo veo imposible, tengo ideas. Pero no sé cómo darles forma al mundo donde transcurren.
#speaker:Tolkien #icon:tolkien_misterioso
Construye tu mundo como si hubiera existido antes de ti.
Dale historia… lenguas… mitos… haz que parezca real, aunque no lo sea.
Un mundo se siente real cuando parece que existía antes de que lo contaras.
#speaker:Fantasma #icon:fantasma_mindBlowing
Es como intentar contar una historia que ya ha ocurrido, pero teniendo en cuenta que nadie comprende cómo es ese mundo, ¿no?
#speaker:Tolkien #icon:tolkien_normal
Exactamente. Cuenta la historia… y deja que el lector descubra el mundo.
Veo que vas pillando la idea. Dicho esto vamos a impulsar tus poderes. #OPEN_UI
-> eleccion_poder


=== eleccion_poder ===
// Cuando Unity asigne la variable 'poder_elegido' y continúe, evaluaremos:
#speaker:Tolkien #icon:tolkien_misterioso
{ poder_elegido:
    - "venganza":
        En muchas historias, los héroes caen antes de poder completar su misión.
        Sus espíritus quedan atrapados entre las páginas, recordando las batallas que nunca pudieron ganar.
        Cuando uno de tus enemigos caiga, las almas de héroes olvidados aparecerán para vengar aquello que ellos mismos no pudieron vengar.
        No buscan gloria… solo justicia.
    
    - "batalla":
        Existe una historia que casi nadie recuerda.
        En ella se habla de un anillo que no corrompe… sino que despierta el poder de quien lucha con determinación.
        No pertenece a ningún reino… porque nació antes que todos ellos. 
        Es una reliquia nacida de esas historias que nunca llegaron a ver la luz.
        Las historias tienen eco… y este poder es muestra de ello.
    
    - "salvacion":
        Incluso en los momentos más oscuros… siempre existe una luz.
        Un viejo mago solía decir que la esperanza aparece justo cuando parece imposible encontrarla.
        La luz de su sabiduría no es eterna...
        pero a veces unos segundos bastan para cambiar el curso de una historia.
}
-> finalizacion


=== finalizacion ===
#speaker:Tolkien #icon:tolkien_normal
// Despedida aleatoria
{~ El poder que te he prestado no es mío, sino de las historias que lo inspiraron. Úsalo sabiamente. | Incluso la más pequeña de las criaturas puede cambiar el curso del destino. | Ahora ve. El juicio aún te espera. }
-> END