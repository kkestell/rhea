rhea:
	dotnet build

example:
	dotnet run example.rhea --project Rhea
	clang example.c -L./LibRhea/bin -I./LibRhea -lgc -lrhea -o example

clean:
	rm -f test.c
	rm -f test

.PHONY: rhea test clean