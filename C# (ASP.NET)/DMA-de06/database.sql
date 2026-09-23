-- Create the RecipeManagement database
CREATE DATABASE RecipeManagement;
GO

-- Use the RecipeManagement database
USE RecipeManagement;
GO

-- Create the Recipes table
CREATE TABLE Recipes (
    RecipeId INT PRIMARY KEY IDENTITY(1,1),
    RecipeName VARCHAR(100) NOT NULL,
    CookingTime INT NOT NULL, -- in minutes
    Description TEXT
);
GO

-- Create the Ingredients table
CREATE TABLE Ingredients (
    IngredientId INT PRIMARY KEY IDENTITY(1,1),
    IngredientName VARCHAR(100) NOT NULL,
    Quantity DECIMAL(10, 2) NOT NULL, -- amount of ingredient
    RecipeId INT,
    FOREIGN KEY (RecipeId) REFERENCES Recipes(RecipeId) ON DELETE CASCADE
);
GO

-- Insert sample data into Recipes table
INSERT INTO Recipes (RecipeName, CookingTime, Description) 
VALUES 
('Spaghetti Carbonara', 25, 'A classic Italian pasta dish with bacon, eggs, and cheese.'),
('Chicken Curry', 45, 'A flavorful curry dish with tender chicken and rich spices.'),
('Chocolate Cake', 60, 'A moist chocolate cake with a rich chocolate frosting.');

-- Insert sample data into Ingredients table
INSERT INTO Ingredients (IngredientName, Quantity, RecipeId) 
VALUES 
('Spaghetti', 500.00, 1), -- RecipeId 1 refers to Spaghetti Carbonara
('Eggs', 4.00, 1),
('Bacon', 200.00, 1),
('Chicken Breast', 600.00, 2), -- RecipeId 2 refers to Chicken Curry
('Curry Powder', 50.00, 2),
('Coconut Milk', 400.00, 2),
('Flour', 300.00, 3), -- RecipeId 3 refers to Chocolate Cake
('Sugar', 200.00, 3),
('Cocoa Powder', 100.00, 3);

-- Select all data from Recipes table
SELECT * FROM Recipes;
GO

-- Select all data from Ingredients table
SELECT * FROM Ingredients;
GO

