#include <string.h>
#include <ctype.h>
#include <assert.h>

#include "string.h"

#include "gc.h"

string* string__new(char* val)
{
  string* s = (string*)GC_MALLOC(sizeof(string));
  s->len = strlen(val);
  s->data = (char*)GC_MALLOC_ATOMIC(sizeof(char) * s->len);
  strcpy(s->data, val);
  return s;
}

string* string__dup(string* str)
{
  return string__new(str->data);
}

string* string__trim(string* str)
{
  assert(str->len > 0);

  int so = 0;
  int eo = str->len - 1;

  for(int i = 0; i < str->len; i++) {
    if(isspace(str->data[i]))
      so++;
    else
      break;
  }

  for(int i = str->len - 1; i > 0; i--) {
    if((int)isspace(str->data[i]))
      eo--;
    else
      break;
  }

  assert(eo > so);

  int newLen = (eo - so) + 1;

  string* newStr = (string*)GC_MALLOC(sizeof(string));

  newStr->len = newLen;
  newStr->data = (char*)GC_MALLOC_ATOMIC(sizeof(char) * newStr->len);

  for(int i = so; i <= eo; i++) {
    newStr->data[i - so] = str->data[i];
  }

  return newStr;
}

int64_t string__length(string* str)
{
  return str->len;
}