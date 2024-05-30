INCLUDE ../../globals.ink

VAR quest3 = false
~ quest3 = isQuestStartable("Quest3")

VAR quest5 = false
~ quest5 = isQuestStartable("Quest5")

VAR passedStart = false

-> Start
== Start ==
O czym { passedStart: jeszcze } chciałeś porozmawiać?
~ passedStart = true
{ quest3:
+ O zadaniu 3
-> Quest3Startable
}
{ quest5:
+ O zadaniu 5
-> Quest5Startable
}
+ Nie ważne (zakończ)
-> DONE

== Quest3Startable ==
Potrzebujemy twojej pomocy z odparciem najazdu czerwonych. Czy podejmujesz się wyzwania?
+ tak
~startQuest("Quest3")
~ quest3 = false
Świetnie!
-> Start
+ nie
Szkoda
-> Start

== Quest5Startable ==
Nasi wrogowie coś knują. Trzeba to zbadać. Czy podejmujesz się wyzwania?
+ tak
~startQuest("Quest5")
~ quest5 = false
Świetnie!
-> Start
+ nie
Szkoda
-> Start