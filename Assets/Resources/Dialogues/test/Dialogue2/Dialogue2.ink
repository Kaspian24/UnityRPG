INCLUDE ../../globals.ink

VAR quest2 = false
~ quest2 = isQuestStartable("Quest2")

{ quest2:
-> Quest2Startable
}
Nie mamy o czym rozmawiać.
-> DONE

== Quest2Startable ==
Witaj przybyszu!
Potrzebujemy twojej pomocy z odparciem najazdu pomarańczowych. Czy podejmujesz się wyzwania?
+ tak
~startQuest("Quest2")
Świetnie!
-> DONE
+ nie
Szkoda
-> DONE