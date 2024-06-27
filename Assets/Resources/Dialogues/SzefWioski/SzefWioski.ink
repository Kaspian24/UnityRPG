INCLUDE ../globals.ink

VAR wezwanie = false
~ wezwanie = isDialogueStartable("Szef", "Wezwanie")

VAR szkielety = false
~ szkielety = isDialogueStartable("Szef", "Szkielety")

VAR speedrun = false
~ speedrun = isDialogueStartable("Szef", "Speedrun")

VAR smok = false
~ smok = isDialogueStartable("Szef", "Smok")

{ wezwanie:
-> DialogWezwanie
}

{ szkielety:
-> DialogSzkielety
}

{ smok:
-> DialogSmok
}

Nie mamy o czym rozmawiać.
-> DONE

== DialogWezwanie ==
Naszą wioskę spotyka ostatnio wiele nieszczęść! Najpierw napadały nas szkielety a teraz jeszcze smok!
Czy zechcesz udzielić nam pomocy w obronie naszej wioski?
+ tak
~startQuest("Szkielety")
-> WezwanieZgoda
+ nie
Wróc jeżeli zmienisz zdanie.
-> DONE

== WezwanieZgoda ==
~topicTalkedAbout("Szef", "Wezwanie")
~disableTopic("Szef", "Wezwanie")
Dziękujemy!
W pierwszej kolejności postaraj się pokonać chociaż część z pobliskich szkieletów.
Najbliższy oddział znajduje się za lasem za moim domem.
Weź to wyposażenie, przyda ci się.
-> DONE

== DialogSzkielety ==
Widzę że udało Ci się uporać z kilkoma szkieletami.
Myślę jednak, że powinieneś zdobyć więcej doświadczenia nim zawalczysz ze smokiem.
Nie będę Cię jednak powstrzymywał, smok znajduje się na łące za pojedynczym domem w pobliżu jeziora.
Weź to wyposażenie, bez niego marny twój los w walce ze smokiem.
{ speedrun:
 -> DialogSpeedrun
}
~topicTalkedAbout("Szef", "Szkielety")
~disableTopic("Szef", "Szkielety")
~startQuest("Smok")
Powodzenia!
-> DONE

== DialogSpeedrun ==
+ już go pokonałem
Co?!
~topicTalkedAbout("Szef", "Speedrun")
~disableTopic("Szef", "Speedrun")
~topicTalkedAbout("Szef", "Szkielety")
~disableTopic("Szef", "Szkielety")
~startQuest("UkonczGre")
Z tym uzbrojeniem?! Jak ci się to udało?!
Jesteś prawdziwym bohaterem!
-> DONE

== DialogSmok ==
+ udało mi się pokonać smoka
Wspaniale!
~topicTalkedAbout("Szef", "Smok")
~disableTopic("Szef", "Smok")
Dzięki tobie nasza wioska jest znacznie bezpieczniejsza.
Jesteś prawdziwym bohaterem!
-> DONE