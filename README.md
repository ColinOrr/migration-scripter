migration-scripter
==================

Windows command line tool to script Entity Framework code first migrations.

Usage
-----
```
  migration-scripter <assembly name> -config=<config path>
                     [-contextAssembly=<context assembly name>]
                     [-configurationTypeName=<configuration type name>]
                     [-source=<source migration>][-target=<target migration>]
                     [-connection=<connection name>][-out=<output file>]

where <assembly name> is the name of the assembly containing the migrations
excluding the file extension.

switches:

    -config                 (required) the path to the configuration file which
                            contains the connection string

    -contextAssembly        the name of the assembly containing the database
                            context excluding the file extension. This defaults
                            to the <assembly name> assembly if not supplied.

    -configurationTypeName  the namespace qualified name of the migrations
                            configuration class. If not supplied the migrations
                            assembly will be scanned for a Configuration class.

    -source                 the migration to update from. If no source is
                            supplied, a script to update the current database
                            will be produced.

    -target                 the migration to update to.  If no target is
                            supplied, a script to update to the latest
                            migration will be produced.

    -connection             the name of the connection string in the
                            application configuration if it differs from
                            the <assembly name>.

    -out                    the name of the SQL file to be generated.  If no
                            output is supplied the script will be written to
                            the console.
 ```


License
-------

The MIT License (MIT)

Copyright (c) 2014 Colin Orr

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
