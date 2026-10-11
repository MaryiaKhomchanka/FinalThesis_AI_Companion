from fastapi import FastAPI 

app = FastAPI(title = "AI companion Local Service")

@app.get("/health")
def health():
    return {"status": "ok", "message": "AI companion is running!"}