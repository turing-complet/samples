import sys, os, tty, termios
import readline

# make this evaluate to cwd
prompt = '[pysh]$ '

def chdir(args):
    os.chdir(args[0])

def pwd(args):
    print(os.getcwd())

def echo(args):
    print(args[0])

def ls(args):
    if len(args) == 0:
        args.append(None)
    files = os.listdir(args[0])
    for fName in files:
        print(fName)

builtins = {
    "cd": chdir,
    "echo": echo,
    "pwd": pwd,
    "ls": ls
}

# TODO
# history, pipes, globs, quotes, fg and bg, other builtins, colors, etc

# harder than expected
# history = []

# def read_input():
#     print(prompt, end=" ")
#     fd = sys.stdin.fileno()
#     old_settings = termios.tcgetattr(fd)
#     try:
#         tty.setraw(sys.stdin.fileno())
#         line = sys.stdin.readline()
#     finally:
#         termios.tcsetattr(fd, termios.TCSADRAIN, old_settings)
#     return line


def loop():
    while True:
        cmdline = input(prompt)
        
        if len(cmdline) == 0 or cmdline.isspace():
            continue
        try:
            (cmd, args) = parse_args(cmdline)
            if cmd in builtins.keys():
                builtins[cmd](args)
            else:
                run_cmd(cmd, args)

        except KeyboardInterrupt:
            sys.exit(0)
        except:
            print(sys.exc_info())


def parse_args(cmdline):
    tokens = cmdline.split(" ")
    return (tokens[0], tokens[1:])


def run_cmd(cmd, args):
    
    pid = os.fork()
    if pid == 0:
        os.execvp(cmd, [cmd] + args)
    elif pid < 0:
        print("Error forking process")
    elif pid > 0:
        wpid = os.waitpid(pid, 0)
    

if __name__ == '__main__':
    print('Welcome to pysh')
    loop()