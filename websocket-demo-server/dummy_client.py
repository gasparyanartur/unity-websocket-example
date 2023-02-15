import asyncio
import websockets as ws

async def hello():
    async with ws.connect('ws://127.0.0.1:8005') as socket:
        await socket.send('hello world')
        print(await socket.recv())

asyncio.run(hello())