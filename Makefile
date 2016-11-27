PROJECTS = src/ZNetMonad test/ZNetMonad.Tests
OUT_DIRS = $(patsubst %,%/bin,$(PROJECTS)) $(patsubst %,%/obj,$(PROJECTS))
LOCK_FILES = $(patsubst %,%/project.lock.json,$(PROJECTS))
SOURCES = $(shell find src/ -type f -name '*.cs')

.PHONY: test cleanall

all: test

cleanall: cleanoutput
	@rm -rf $(patsubst %,%/project.lock.json,$(PROJECTS))

cleanoutput:
	@rm -rf $(OUT_DIRS)

$(LOCK_FILES):
	@dotnet restore

test: $(LOCK_FILES) $(SOURCES)
	@dotnet test test/ZNetMonad.Tests
