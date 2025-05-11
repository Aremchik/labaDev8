from fastapi import FastAPI, Depends
from sqlalchemy.orm import Session
from app.database import SessionLocal, engine
from app import models, schemas

models.Base.metadata.create_all(bind=engine)

app = FastAPI()

def get_db():
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()

@app.post("/raw_user/")
def receive_user(data: schemas.RawUserCreate, db: Session = Depends(get_db)):
    raw_user = models.RawUser(**data.dict())
    db.add(raw_user)
    db.commit()
    db.refresh(raw_user)
    return {"status": "received", "id": raw_user.id}
