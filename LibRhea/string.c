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

string* string__dup(string* self)
{
  return string__new(self->data);
}

string* string__trim(string* self)
{
  assert(self->len > 0);

  int so = 0;
  int eo = self->len - 1;

  for(int i = 0; i < self->len; i++) {
    if(isspace(self->data[i]))
      so++;
    else
      break;
  }

  for(int i = self->len - 1; i > 0; i--) {
    if((int)isspace(self->data[i]))
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
    newStr->data[i - so] = self->data[i];
  }

  return newStr;
}

int64_t string__length(string* self)
{
  return self->len;
}