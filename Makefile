rhea:
	dotnet build

test:
	dotnet run test.rhea --project Rhea
	clang test.c -L./LibRhea/bin -I./LibRhea -lgc -lrhea -o test

clean:
	rm -f test.c
	rm -f test

.PHONY: rhea test clean