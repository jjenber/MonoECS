# MonoECS
An Entity Component System for making games using [MonoGame]("http://www.monogame.net/").

## What is an Entity Component System?
This framework builds on a lot of the information found here [http://entity-systems.wikidot.com/]("http://entity-systems.wikidot.com/"). In short: Entities are nothing more than an ID and containers of components. Components are just data, no logic! The logic is done in systems. Systems finds and updates entities with the components that concern them using ComponentMatchers. 

## Version 0.1
This is very much a work in progress and right now the bare minimum for an ECS framework. It will continue to be updated though. The current version includes a Core project containing all code for the ECS. It also includes a minimal Test project to show an example of how the framework can be used.  

## Plans as of now:
- Better component matchers with more variations.
- Reactive systems.
- XML documentation.
- Wiki guide.
