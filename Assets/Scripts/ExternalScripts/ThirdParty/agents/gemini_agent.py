from pydantic import BaseModel
import google.generativeai as genai
from uagents import Agent, Context
from uagents.setup import fund_agent_if_low
from uagents import Model
from langchain.prompts import PromptTemplate
from langchain_google_genai import ChatGoogleGenerativeAI
from langchain_core.output_parsers import StrOutputParser
import os
os.environ["GOOGLE_API_KEY"] = "AIzaSyAxm_HLuC5oVvpIAHS99ybuHHovsGaMYbw"

# Define a model for messages
class Message(Model):
    message: str

# Define the user agent
Gemini_agent = Agent(
    name="Clerk",
    port=8001,
    seed="Gemini Agent secret phrase",
    endpoint=["http://localhost:8001/submit"],
)

 
# Fund the user agent if its wallet balance is low
fund_agent_if_low(Gemini_agent.wallet.address())

# Configure the API key for Google's generative AI service
genai.configure(api_key='AIzaSyAxm_HLuC5oVvpIAHS99ybuHHovsGaMYbw') # replace your gemini API key here

# Initialize the generative model with a specific model name
model = genai.GenerativeModel(model_name="models/gemini-1.5-pro-latest",
                              system_instruction= "You're a store clerk. Help someone learn a language they ask for based on the responses they give. Communicate with them in that language. They'll be completing a shopping trip at your store, speak to them and try to help them learn")

# Starting a new chat session
chat = model.start_chat(history=[])


# Function to handle incoming messages
async def handle_message(message):
    while True:
        # Get user input
        user_message = message
        
        # Check if the user wants to quit the conversation
        if user_message.lower() == 'quit':
            return "Exiting chat session."
            
        # Send the message to the chat session and receive a streamed response
        response = chat.send_message(user_message, stream=True)
        
        # Initialize an empty string to accumulate the response text
        full_response_text = ""
        
        # Accumulate the chunks of text
        for chunk in response:
            full_response_text += chunk.text
            
        # Print the accumulated response as a single paragraph
        message = "Clerk: " + full_response_text
        return message

# Event handler for agent startup
@Gemini_agent.on_event('startup')
async def address(ctx: Context):
    # Logging the agent's address
    ctx.logger.info(Gemini_agent.address)
    # Get a response from LangChain using the persona story

# Handler for query given by user
@Gemini_agent.on_message(model=Message)
async def handle_query_response(ctx: Context, sender: str, msg: Message):
    # Handling the incoming message
    message = await handle_message(msg.message)
    # Logging the response
    ctx.logger.info(message)
    # Sending the response back to the sender
    await ctx.send(sender, Message(message=message))

    