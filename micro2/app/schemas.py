from pydantic import BaseModel

class RawUserCreate(BaseModel):
    name: str
    email: str
