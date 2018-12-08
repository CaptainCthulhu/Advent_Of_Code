using System;
using System.Collections.Generic;
using System.Linq;

namespace _7_1
{
    class Element
    {
        public static List<Element> elements;

        public string name;
        public List<Element> children;
        public List<Element> parents;
        public Boolean traversed = false;
        public Boolean working = false;
        public int StepLength;
        public int TimeSpent = 0;

        public static void FindPath()
        {
            PopulateElements();
            List<Element> firstElements = Element.FindFirstElements();

            string path = "";
            foreach (Element element in firstElements)
                path += Element.Traverse(element);

            Console.WriteLine(path);
        }

        public static Boolean HasUnfinishedElements
        {
            get
            {
                var elements = from element in Element.elements
                                where element.Finished == false
                                select element;
                return elements.Count() > 0;
            }
        }

        public static void PopulateElements()
        {
            elements = new List<Element>();
            string[,] workingSet = input.working;

            for (int i = 0; i < workingSet.GetLength(0); ++i)
            {
                Element element = FindOrCreateElement(workingSet[i, 0]);
                Element childElement = FindOrCreateElement(workingSet[i, 1]);
                element.AddChild(childElement);
                childElement.AddParent(element);

                if (!Element.elements.Contains(element))
                    Element.elements.Add(element);

                if (!Element.elements.Contains(childElement))
                    Element.elements.Add(childElement);

            }
        }

        static Element FindOrCreateElement(string name)
        {
            var elementsWithName = from element in Element.elements
                                   where element.name == name
                                   select element;
            if (elementsWithName.Count() > 0)
                return elementsWithName.First();
            else
                return new Element(name);
        }

        public Boolean HasNoParents
        {
            get
            {
                return (parents.Count == 0) ? true : false;                    
            }
        }

        public Boolean Finished
        {
            get
            {
                return StepLength == TimeSpent;
            }
        }

        public Boolean HasUnTraversedParents
        {
            get
            {
                if (parents.Count > 0)
                {
                    var unTraversedParents = from parent in parents
                                             where !parent.traversed 
                                             select parent;
                    if (unTraversedParents.Count() > 0)
                        return true;
                }
                return false;
            }
        }

        public static List<Element> UnWorkedElements()
        {
            List<Element> elements = UnprocessedElements();
            var unWorkedElements = from element in elements
                                   where !element.working
                                   select element;
            return unWorkedElements.ToList<Element>();
        }

        public static List<Element> UnprocessedElements()
        {
            List<Element> elements = Element.elements;
            var unprocessedElements = from element in elements
                                      where !element.HasUnTraversedParents
                                      && !element.Finished
                                      select element;
            return unprocessedElements.OrderBy(element => element.name).ToList<Element>();
        }

        public Element(string name)
        {
            this.name = name;
            children = new List<Element>();
            parents = new List<Element>();
            StepLength = input.BaseTime + char.ToUpper(Convert.ToChar(name)) - 64;
        }

        public void AddChild(Element child)
        {
            children.Add(child);
            children = children.OrderBy(x => x.name).ToList<Element>();
        }

        public void AddParent(Element parent)
        {
            parents.Add(parent);
            parents = parents.OrderBy(x => x.name).ToList<Element>();
        }

        public static List<Element> FindFirstElements()
        {
            var elements = Element.elements;
            return (from element in elements
                    where element.HasNoParents
                    select element).ToList<Element>().OrderBy(x => x.name).ToList<Element>();
        }

        public static string Traverse(Element element)
        {
            string path = "";
            if (element.HasUnTraversedParents || element.traversed)
                return "";

            element.traversed = true;
            
            path = element.name;

            List<Element> unprocessedElements = Element.UnprocessedElements();
            foreach (Element unprocessedElement in unprocessedElements)
            {
                path += Traverse(unprocessedElement);
            }         

            return path;
        }
            
    }
}
