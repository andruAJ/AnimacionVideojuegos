# Equipos e integrantes:

  Animación para videojuegos: Andrés Juan Giraldo Vargas, Juan Guillermo Caicedo, Gustavo Adolfo Lora, Juan Andrés Gaviria.

  Computación Gráfica: Andrés Juan Giraldo Vargas y Jacobo Rodríguez. 

# Entrega Final - Hack n Slash ARPG con Power Ups (Patrón Decorator)

Demo jugable tipo hack n slash construida sobre el controlador ARPG visto en clase.  
El objetivo es mostrar un sistema de oleadas infinitas, cambio de personaje, combate ARPG y power ups implementados con el patrón de diseño **Decorator**, integrando control, animación, IA y diseño modular.

---

# Controles:

  WASD - Movimiento
  QE - Rotación
  Click Izq. - Ataque rápido
  Click Der. - Ataque Pesado

## Objetivo de la entrega

Construir una demo jugable tipo hack n slash utilizando el controlador ARPG del curso, con:

- Sistema de oleadas infinitas de enemigos.
- Capacidad de intercambiar personajes jugables en tiempo real.
- Combate ARPG funcional con ataques, combos y detección de daño.
- Power ups recolectables implementados mediante el patrón Decorator.
- UI mínima que permita entender el estado del jugador y del combate.

El foco del proyecto es demostrar una arquitectura limpia y extensible que se pueda ampliar sin modificar la lógica base del jugador.

---

## Alcance mínimo implementado

### 1. Oleadas infinitas

- Spawner de enemigos con generación progresiva.  
- Al menos dos tipos de enemigo con comportamientos diferenciados.  
- Incremento gradual de dificultad (por ejemplo número de enemigos, velocidad o daño).  
- Condición de Game Over cuando la vida del jugador llega a cero.

### 2. Intercambio de personajes (desactivado)

- Al menos dos personajes jugables basados en el controlador ARPG del curso.  
- Cambio de personaje en tiempo real durante la partida.  
- Cada personaje conserva sus propios atributos de vida, daño y velocidad.

### 3. Combate ARPG

- Ataques básicos y pesados con animaciones sincronizadas.  
- Detección de impacto sobre enemigos.  
- Enemigos que persiguen y atacan activamente al jugador.  
- Muerte del jugador que dispara Game Over o reinicio de oleadas.

### 4. Sistema de power ups con patrón Decorator

- Power ups recolectables en el escenario.  
- Al recogerlos se aplica un efecto temporal o acumulable sobre el jugador.  
- Implementación basada en una interfaz o clase base de estadísticas y decoradores que extienden su comportamiento sin modificar la clase base.  
- Feedback visual o sonoro para cada power up activo o recogido.

### 5. Interfaz mínima

- Barra de vida del jugador.  
- Contador de oleadas y/o enemigos derrotados.  
- Indicador visible de power ups activos y su duración aproximada.

---

## Estructura del proyecto

- Rama de trabajo recomendada: `main`  
- Carpeta principal de la demo: `Assets/EjercicioFinal`  
  - Escenas de juego.  
  - Prefabs de jugador, enemigos y power ups.  
  - Scripts de oleadas, combate, VFX y UI.

Ajusta estos nombres si tu estructura final difiere.

---

## Videos VFX

1. Pick Ups:


 
2. Ataque cargado:



3. Ataque rápido:

