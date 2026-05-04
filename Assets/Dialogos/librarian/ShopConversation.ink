-> shop_entrance

=== shop_entrance ===
// Ink detecta automáticamente si ya hemos leído el nudo 'first_meeting'. 
// Si es verdad, va a los aleatorios. Si no, va al primer encuentro.
{ first_meeting: 
    -> random_meeting 
- else: 
    -> first_meeting 
}

=== first_meeting ===
#canvas:Fantasy #speaker: Bibliotecaria #icon: librarian_normal
Bienvenido a la tienda, que es lo que quieres?
#speaker: Fantasma #icon: fantasma_mindBlowing
WOWWWW.
#speaker: Fantasma #icon: fantasma_confundido
Pero que vendes, ¿¿ libros???
#speaker: Bibliotecaria #icon: librarian_ironica
…
Te recuerdo que estamos en una biblioteca, aquí el conocimiento es gratuito.
#speaker: Bibliotecaria #icon: librarian_normal
Lo que no es gratuito es saber hasta dónde has leído.
#speaker: Fantasma #icon: fantasma_normal
Es decir, vendes marcapáginas…
#speaker: Bibliotecaria #icon: librarian_normal
No, vendemos croquetas. ¿De que las quieres de jamón o de cocido?
#speaker: Fantasma #icon: fantasma_feliz
¿En serio?
#speaker: Bibliotecaria #icon: librarian_normal
No.
Si que eres ingenuo pequeño. Si no corriges eso, te va a dar problemas.
Sí, vendemos marcapáginas, son nuestros bestsellers.
¿Quieres uno… o solo vienes a mirar?
-> end_dialogue

=== random_meeting ===
// El símbolo '~' hace un "shuffle". Elige una ruta al azar cada vez que entras.
{~ -> var1 | -> var2 | -> var3}

= var1
#speaker: Bibliotecaria #icon: librarian_normal
Hombre, si estás de vuelta. ¿Que ha pasado pequeño, aún no has ganado tu lugar en estas estanterías?
#speaker: Fantasma #icon: fantasma_cansado
No.
#speaker: Fantasma #icon: fantasma_feliz
Estoy en ello, soy muy pesado, es una de mis pocas cualidades.
#speaker: Bibliotecaria #icon: librarian_pesada
Persistente, pequeño, persistente.
#speaker: Bibliotecaria #icon: librarian_pesada
La base de todo buen escritor es su léxico.
#speaker: Fantasma #icon: fantasma_cansado
Tengo un léxico increíbleeeee.
#speaker: Fantasma #icon: fantasma_normal
Solo que lo reservo para momentos especiales.
#speaker: Fantasma #icon: fantasma_feliz
Me gusta sorprender con mi elocuencia.
#speaker: Bibliotecaria #icon: librarian_ironica
Sí… Sorprendes… pero por otros motivos. 
#speaker: Bibliotecaria #icon: librarian_normal
¿Quieres comprar algo hoy?
O mejor aún… ¿ya sabes qué tipo de escritor intentas ser?
-> end_dialogue

= var2
#speaker: Fantasma #icon: fantasma_normal
He vuelto, señora bibliotecaria.
#speaker: Bibliotecaria #icon: librarian_normal
Ya lo veo. Vienes tanto que voy a empezar a pensar que no sabes leer sin ellos.
Mis marcapáginas deben de estar volviéndose imprescindibles.
#speaker: Fantasma #icon: fantasma_feliz
Sí, son muy bonitos.
#speaker: Bibliotecaria #icon: librarian_ironica
Bonitos… sí
Útiles… también.
#speaker: Bibliotecaria #icon: librarian_normal
Pero eso no explica por qué sigues volviendo.
¿Buscas algo… o solo estás evitando encontrarlo?
¿Quieres más? ¿Como snack o como plato fuerte?
-> end_dialogue

= var3
#speaker: Bibliotecaria #icon: librarian_normal
Bienvenido a mi stand, que quieres comprar hoy.
#speaker: Fantasma #icon: fantasma_confundido
Pues, pues, no lo tengo muy claro. ¿Cuál te gusta más a ti señora bibliotecaria?
#speaker: Bibliotecaria #icon: librarian_normal
Yo no tengo favoritos.
Los favoritos no sirven para crecer.
#speaker: Bibliotecaria #icon: librarian_bromista
Pero si alguno hablara… el cuarto pediría perdón constantemente.
Y aun así, aprenderías algo de él.

-> end_dialogue

=== end_dialogue ===
// Esta etiqueta (tag) le servirá a Unity para saber que toca abrir la tienda
# OpenShop
-> END