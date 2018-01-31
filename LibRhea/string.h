#ifndef STRING_H
#define STRING_H

#include <stdbool.h>
#include <stdint.h>
#include <stdio.h>
#include <stdlib.h>

typedef struct {
  char* data;
  size_t len;
} string;

string* string__new(char* val);
string* string__dup(string* str);
string* string__trim(string* str);
int64_t string__length(string* str);

#endif