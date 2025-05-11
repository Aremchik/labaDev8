from sqlalchemy import Column, Integer, String
from app.database import Base

class RawUser(Base):
    __tablename__ = "raw_user_data"

    id = Column(Integer, primary_key=True, index=True)
    name = Column(String)
    email = Column(String)
