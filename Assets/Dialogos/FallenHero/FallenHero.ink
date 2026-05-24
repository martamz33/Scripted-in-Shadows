// Variable que recibirá el valor desde el GameManager en Unity
VAR boss_wins = 0

=== intro_boss ===
#canvas:Fantasy #speaker:Fallen Hero #icon:fallenHero_normal
No te la llevarás.

#speaker:Fantasma #icon:confundido
¿Eh?

# START_BOSS
-> END

=== dead_fallenHero ===
#speaker:Fallen Hero
{
    - boss_wins == 0:
        Te veo firme en tu causa…
        y recuerdo un reino bañado por la luz.
        Una paz que creí eterna.
        Una época en que no tenía mayores preocupaciones.

    - boss_wins == 1:
        Fui un caballero.
        Uno de los mejores.
        Mi espada nunca temblaba…
        hasta que aparecía ella.
        Entonces el mundo se diluía y solo la veía a ella.

    - boss_wins == 2:
        No era el honor lo que guiaba mi espada…
        ni la gloria…
        era ella.
        Solo ella era mi razón para luchar.

    - boss_wins == 3:
        Donde ella caminaba, yo vigilaba en silencio.
        Donde ella sonreía… encontraba un motivo para seguir luchando.
        Cuando ella hablaba, yo solo podía mirarla embobado.

    - boss_wins == 4:
        Nunca se lo dije.
        No fui lo suficientemente valiente.
        Pero juré protegerla… incluso del destino mismo.
        Jure ser la sombra protectora, que velará por ella.

    - boss_wins == 5:
        Tú luchas por algo… puedo verlo.
        Yo también lo hice.
        Pero a veces querer algo no es suficiente.
        Eso lo entendí el día que… la arrebataron.

    - boss_wins == 6:
        En la noche más oscura… desapareció.
        Sin que pudiera hacer nada.
        Y con ella… todo lo que daba sentido a mi juramento.
        A mi vida.

    - boss_wins == 7:
        El rey ordenó esperar.
        Dijo que la guerra solo provocaría más muerte…
        que negociar era el único camino.
        Que ir a la muerte solo conseguiría que ella desapareciera de este mundo.

    - boss_wins == 8:
        Escuché sus palabras…
        y las rechacé.
        Porque hay órdenes que un corazón no puede obedecer.
        Antes que mi juramento como caballero, estaba ella.
        Siempre lo estará.

    - boss_wins == 9:
        Fui solo. 
        Sin honor… sin permiso… sin miedo.
        Solo con la esperanza de encontrarla con vida.

    - boss_wins == 10:
        Y la encontré.
        Encadenada… herida…
        pero viva.
        Aún viva.
        En ese momento la esperanza se encendió en mi pecho.

    - boss_wins == 11:
        Nuestras miradas se cruzaron…
        y por un instante… todo volvió a tener sentido.
        Como ahora… cuando te miro.
        Y recuerdo aquella época.

    - boss_wins == 12:
        Pero no fui lo bastante rápido.
        No fui lo bastante fuerte.
        Sentí el acero atravesarme…
        vi mi brazo caer…
        y aun así… no frene, seguí intentando alcanzarla.

    - boss_wins == 13:
        Pero la arrojaron al vacío…
        y en ese instante… el mundo dejó de existir.
        Si hubieras estado allí… tampoco habrías podido salvarla.
        O eso quiero pensar.
        Solo pensar que otra persona hubiera podido salvarla...
        hace que el sentimiento de culpabilidad sea más grande que mi amor por ella.

    - boss_wins == 14:
        Lo que vino después… no lo llamaría justicia.
        Ni venganza.
        Fue un hombre roto… matando sin propósito…
        hasta que no quedó nadie.
        Solo la culpa cubriendo su rostro.
        Cuando el rey llegó… no vio a su caballero.
        Vio a un monstruo… y al culpable de la muerte de su hija.
        No me defendí.
        No lo negué.
        Solo acepté mi destino…
        como acepto ahora enfrentarme a ti.

    - boss_wins == 15:
        Así que esto es el final…
        He luchado, he matado… he caído más bajo de lo que jamás imaginé que un alma podía caer.
        Y aun así… nada de ello cambió lo único que importa.
        No pude salvarla.
        No fui suficiente.
        Fallé como caballero.
        Fallé como hombre.
        Fallé como amante.
        Y desde entonces… cada golpe, cada alma, cada batalla…
        no ha sido más que un intento inútil de ahogar ese instante.
        De intentar olvidar que ella fue arrebata de mis brazos justo delante de mi.
        ...
        Y sin embargo…
        Aun la siento.
        Aquí.
        Como si nunca se hubiera ido.
        Como si siguiera a mi lado a pesar de todo.
        ...
        Dime…
        ¿es esto castigo…
        o es que ella… nunca me abandonó?
        Si es así…
        entonces este peso…
        este dolor…
        es lo único que aún me mantiene a su lado.

    - boss_wins >= 16:
        Ya por favor... no puedo más.
        Perdóname…
        No por haber luchado…
        sino por no haber sido suficiente.
        ...
        Si aún puedes verme…
        si de verdad sigues aquí…
        Entonces no me dejes olvidar.
        No dejes que este dolor desaparezca.
        Porque es lo único que me queda de ella.
        Lo único que aun me ata a ella.

        #speaker:Fantasma #icon:normal
        Ella siempre te acompaña.
}
-> END