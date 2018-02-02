all: rhea.h string.c string.h
	rm -rf bin
	mkdir bin
	clang -c string.c
	ar -rv bin/librhea.a string.o
	rm -f *.o

clean:
	rm -rf bin
