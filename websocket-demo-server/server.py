import asyncio
import websockets as ws


async def serve_socket(socket):
    async for msg in socket:
        print(msg)
        await socket.send(msg)


async def run_server():
    async with ws.serve(serve_socket, '127.0.0.1', 8005):
        await asyncio.Future()


def main():
    asyncio.run(run_server())


if __name__ == '__main__':
    main()