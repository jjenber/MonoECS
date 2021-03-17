# MonoECS
An Entity Component System for making games using [MonoGame]("http://www.monogame.net/").
Developed as a learning exercise using a hybrid like model. Focus is more on ease of use than performance.
It includes a simplified Arkanoid game as a small test.
## What is an Entity Component System?
This framework builds on a lot of the information found here [http://entity-systems.wikidot.com/]("http://entity-systems.wikidot.com/"). In short: Entities are nothing more than an ID and containers of components. Components are just data, no logic. The logic is done in systems. Systems finds and updates entities with the components that concern them using ComponentMatchers. 


