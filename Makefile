rhea:
	make -C Rhea
	make -C LibRhea

test:
	make -C Test

example:
	dotnet run example.rhea --project Rhea
	clang example.c -Werror -Wno-parentheses-equality -L./LibRhea/bin -I./LibRhea -lgc -lrhea -o example

clean:
	make clean -C Rhea
	make clean -C LibRhea
	rm -f example.c
	rm -f example

.PHONY: rhea test clean example